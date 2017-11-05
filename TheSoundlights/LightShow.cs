using System;
using System.Windows.Forms;
using System.Drawing;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;

using NAudio.Wave;
using NAudio.Dsp;

namespace TheSoundlights
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct UnionStruct
    {
        [FieldOffset(0)]
        public byte[] bytes;
        [FieldOffset(0)]
        public float[] floats;
    }
    public partial class MainForm : Form
    {
        class LightShow
        {
            // количество каналов
            public const int numBands = 4;

            // значения границ частотного диапазона для вычисления амплитуды каналов
            int[] bandDefs = new int[numBands - 1] { 170, 350, 1000 };

            static int spectrumBands = 16;
            int spectrumBandChartSize = 16;
            float[] spectrumStates = new float[spectrumBands];
            int[] spectrumBandDefs = new int[spectrumBands - 1];

            // шаблон посылки (новая и старая версии имеют разный тип команд)
            const string packet = "set rgb";

            IWaveIn waveIn;

            // степень двойки для БПФ
            const int log2N = 12;
            // размер буфера крайтный степени двойки
            const int size = 1 << log2N;
            //
            float sampleRate;

            // уровень усиления
            float gain = 1f;
            // требуемый уровень амплитуды
            const float targetAmplitude = 1f;
            // скорость повышения уровня АРУ
            const float upRegulatorRatio = 0.48f;
            // скорость понижения уровня АРУ
            const float downRegulatorRatio = 0.48f;

            // входной уровень шума
            public float inNoiseLevel = 0.0002f;
            // выходной уровень шума
            public float outNoiseLevel = 4e-5f;

            // время ожидания fading'а
            const double downtimeInSeconds = 5.0;

            // значения яркости выходных каналов
            public float[] outStates = new float[numBands];
            public float[] outScales = new float[numBands];

            // массив из двух цветов для fading'а
            int[,] colors = new int[2, numBands];
            // количество шагов переливания цвета и текущий шаг
            int steps = 255, step = 0;

            Chart spectrumChart = null;
            MyBluetooth myBt;
            public TrackBar[] bars;

            Random rnd;
            Timer rgbTimer;
            Stopwatch time;

            CheckBox fadeCheckBox = null, barsCheckBox = null, lightCheckBox = null;

            public LightShow(MyBluetooth myBt, Chart spectrumChart, TrackBar[] bars, CheckBox fadeCheckBox, CheckBox barsCheckBox, CheckBox lightCheckBox)
            {
                rnd = new Random();
                rgbTimer = new Timer();
                time = new Stopwatch();

                this.spectrumChart = spectrumChart;

                this.myBt = myBt;
                this.bars = bars;

                ToolTip tip = new ToolTip();
                tip.SetToolTip(bars[0], "Уровень красного канала");
                tip.SetToolTip(bars[1], "Уровень оранжевого канала");
                tip.SetToolTip(bars[2], "Уровень зелёного канала");
                tip.SetToolTip(bars[3], "Уровень синего канала");

                this.fadeCheckBox = fadeCheckBox;
                this.barsCheckBox = barsCheckBox;
                this.lightCheckBox = lightCheckBox;

                for (int i = 0; i < numBands; i++)
                    outScales[i] = 1200f;

                spectrumChart.BorderlineWidth = 1;
                spectrumChart.Visible = false;

                spectrumChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
                spectrumChart.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                spectrumChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                spectrumChart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

                spectrumChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
                spectrumChart.ChartAreas[0].AxisX.Maximum = spectrumBands - 0.5;
                spectrumChart.ChartAreas[0].AxisX.Minimum = -0.5;
                spectrumChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
                spectrumChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                spectrumChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;

                spectrumChart.Series["FFT"].ChartType = SeriesChartType.Column;

                spectrumChart.Resize += spectrumChartResize;

                rgbTimer.Interval = 10;
                rgbTimer.Tick += RgbTimer_Tick;
                rgbTimer.Start();
            }

            public void Start()
            {
                // отображаем поля для отправки данных
                visiblingComponents(true, barsCheckBox, fadeCheckBox, lightCheckBox);
                visiblingComponents(Properties.Settings.Default.spectrum, spectrumChart);

                // всегда отображаем трекбар для красного, зелёного и синего каналов. 
                if (Properties.Settings.Default.channelsNumber == numBands)
                    visiblingComponents(true, bars[3], bars[2], bars[1], bars[0]);
                else
                    visiblingComponents(true, bars[3], bars[2], bars[0]);

                // если при включении попали на режим постоянного свечения, то устанавливаем значения выходных каналов
                if (lightCheckBox.Checked)
                    for (int i = 0; i < numBands; i++)
                        bars[i].Invoke(new Action(() => outStates[i] = (bars[i].Value + 5) * 25.5f));

                // стартуем на "прослушку" выходного потока аудио
                waveIn = new WasapiLoopbackCapture();
                waveIn.DataAvailable += WaveIn_DataAvailable;
                waveIn.RecordingStopped += WaveIn_RecordingStopped;
                waveIn.StartRecording();

                while (sampleRate == 0)
                    Application.DoEvents();

                if (spectrumChart.Width > 0)
                    calcSpectrumDefs();
            }

            private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
            {
                int samplesRecorded = e.BytesRecorded / (waveIn.WaveFormat.BitsPerSample / 8);
                int numChannels = waveIn.WaveFormat.Channels;
                sampleRate = waveIn.WaveFormat.SampleRate;

                UnionStruct u = new UnionStruct();
                u.bytes = e.Buffer;

                float currentAmplitude = 0;
                int j = 0;

                Complex[] data = new Complex[size];

                for (int i = 0; i < samplesRecorded && j < size; i += numChannels)
                {
                    float left = u.floats[i];
                    float right = u.floats[i + (numChannels - 1)];
                    float avg = (left + right) / 2;

                    if (Math.Abs(avg) > currentAmplitude)
                        currentAmplitude = Math.Abs(avg);

                    data[j].X = avg * blackmanWindow(j, size);
                    data[j++].Y = 0;
                }

                for (int i = 0; i < spectrumStates.Length; i++)
                    spectrumStates[i] = 0;

                // если на входе не шум, то обрабатываем полученный сигнал
                if (currentAmplitude > inNoiseLevel)
                {
                    time.Reset();

                    // AGC
                    float regulatorRatio = currentAmplitude * gain < targetAmplitude ? upRegulatorRatio : downRegulatorRatio;
                    gain += regulatorRatio * (targetAmplitude / currentAmplitude - gain);

                    for (int i = 0; i < j; i++)
                        data[i].X *= gain;

                    // остаток наполняем нулями
                    for (; j < size; j++)
                        data[j].X = data[j].Y = 0;

                    // выполняем быстрое преобразование Фурье
                    FastFourierTransform.FFT(true, log2N, data);

                    if (Properties.Settings.Default.spectrum)
                        Spectrum(data);

                    if (!lightCheckBox.Checked)
                    {
                        switchOff();

                        int band = 0;

                        for (int i = 0; i < size / 2; i++)
                        {
                            float currentFreq = sampleRate * i / size;

                            if (band < numBands - 1 && currentFreq > bandDefs[band])
                                band++;

                            float amp = data[i].X * data[i].X + data[i].Y * data[i].Y;

                            if (amp > outNoiseLevel)
                                outStates[band] += amp;
                        }

                        // если число каналов 3, а не 4, то добавим амплитуду оранжевого канала к красному
                        if (Properties.Settings.Default.channelsNumber == 3)
                            outStates[0] += outStates[1];

                        for (int i = 0; i < numBands; i++)
                        {
                            float correction = 1f;
                            bars[i].Invoke(new Action(() => correction = (bars[i].Value + 5) / 5.0f));
                            outStates[i] = (float)(Math.Sqrt(outStates[i]) * outScales[i] * correction);
                        }
                    }
                } else if (!lightCheckBox.Checked)
                {
                    time.Stop();
                    double seconds = time.Elapsed.TotalSeconds;
                    time.Start();

                    if (seconds > downtimeInSeconds && fadeCheckBox.Checked)
                        fading(outStates);
                    else
                        switchOff();
                }

                writeStates();
                SpectrumDraw();
            }

            // Освобождение памяти от объекта Wave
            private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
            {
                waveIn.Dispose();
                waveIn = null;
                time.Stop();
                time.Reset();
            }

            public void Stop()
            {
                // отображаем поля для отправки данных
                visiblingComponents(false, barsCheckBox, fadeCheckBox, lightCheckBox, spectrumChart);
                visiblingComponents(false, bars);

                switchOff();
                writeStates();

                if (waveIn != null)
                    waveIn.StopRecording();
            }

            private void Spectrum(Complex[] data)
            {
                try
                {
                    // *****************************************************************************************
                    Array.Resize(ref spectrumStates, spectrumBands);

                    for (int i = 0; i < spectrumBands; i++)
                        spectrumStates[i] = 0;

                    int spectrumBand = 0;

                    for (int i = 0; i < size / 2; i++)
                    {
                        float currentFreq = sampleRate * i / size;

                        if (spectrumBand < spectrumBands - 1 && currentFreq > spectrumBandDefs[spectrumBand])
                            spectrumBand++;

                        float amp = data[i].X * data[i].X + data[i].Y * data[i].Y;

                        if (amp > outNoiseLevel)
                            spectrumStates[spectrumBand] += amp;
                    }
                } catch (IndexOutOfRangeException)
                {

                }
            }

            private void SpectrumDraw()
            {
                spectrumChart.Invoke(new Action(() => {
                    spectrumChart.Visible = Properties.Settings.Default.spectrum;

                    if (spectrumChart.Visible)
                    {
                        spectrumChart.SuspendLayout();
                        spectrumChart.Series["FFT"].Points.Clear();

                        for (int i = 0; i < spectrumStates.Length; i++)
                            spectrumChart.Series["FFT"].Points.Add(new DataPoint(i, spectrumStates[i]));

                        spectrumChart.ChartAreas[0].AxisX.Maximum = spectrumBands - 0.5;
                        spectrumChart.Update();
                        spectrumChart.Refresh();
                        spectrumChart.ResumeLayout();
                    }
                }));
            }

            // сброс значений выходных каналов цветомуызки
            private void switchOff()
            {
                for (int i = 0; i < numBands; i++)
                    outStates[i] = 0;
            }

            private void writeStates()
            {
                string msg = "";

                msg += " " + limit(outStates[0]);

                if (Properties.Settings.Default.channelsNumber == 4)
                    msg += " " + limit(outStates[1]);

                msg += " " + limit(outStates[2]);
                msg += " " + limit(outStates[3]);

                myBt.write(packet + msg + "\n");
            }

            double binaryRoot(int n, int start, int size, double eps = 0.00001)
            {
                double c = (double)size / start;

                double a = Math.Pow(c / n, 1.0 / (n - 1.0));
                double b = 2;

                double res = (a + b) / 2.0;

                while (b - a > eps)
                {
                    res = (a + b) / 2.0;

                    double y = Math.Pow(res, n) - c * res + c - 1;

                    if (y > 0)
                        b = res;
                    else if (y < 0)
                        a = res;
                    else
                        b = a = res;
                }

                return res;
            }

            private void calcSpectrumDefs()
            {
                spectrumBands = spectrumChart.Width / spectrumBandChartSize;

                Array.Resize(ref spectrumBandDefs, spectrumBands - 1);

                double bandFreq = sampleRate / size;

                double a = binaryRoot(spectrumBands, (int)(50 / bandFreq), size / 2);

                double tmp = 1;
                int sum = 0;

                for (int i = 0; i < spectrumBands - 1; i++)
                {
                    int bandwidth = (int)((int)(50 / bandFreq) * tmp);

                    spectrumBandDefs[i] = (int)(bandFreq * (sum + bandwidth));

                    sum += bandwidth;
                    tmp *= a;
                }
            }

            private void spectrumChartResize(object sender, EventArgs e)
            {
                if (spectrumBands != spectrumChart.Width / spectrumBandChartSize && spectrumChart.Width > 0)
                    calcSpectrumDefs();
            }

            // Функция, возвращающая коэффициент окна Блэкмана
            private float blackmanWindow(int n, int size)
            {
                return (float)(0.42 - 0.5 * Math.Cos(2 * Math.PI * n / (size - 1)) + 0.08 * Math.Cos(4 * Math.PI * n / (size - 1)));
            }

            // Функция ограничивает вещественное число v максимальным значением 255 и переводит его в байт
            byte limit(float v)
            {
                return (byte)(v > 255 ? 255 : v);
            }

            long map(long x, long in_min, long in_max, long out_min, long out_max)
            {
                return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
            }

            // Плавное переливание цветом, генерация случайного нового цвета и количества шагов переливания
            private void RgbTimer_Tick(object sender, EventArgs e)
            {
                if (step > steps)
                {
                    step = 0;

                    for (int i = 0; i < numBands; i++)
                    {
                        colors[0, i] = colors[1, i];
                        colors[1, i] = rnd.Next(0, 255);
                    }

                    steps = rnd.Next(40, 256);
                }

                step++;
            }

            // Процедура распределяющая процедуры по цветовым эффектам
            private void fading(float[] ch)
            {
                for (int i = 0; i < numBands; i++)
                    ch[i] = map(step, 0, steps, colors[0, i], colors[1, i]);
            }
        }
    }
}