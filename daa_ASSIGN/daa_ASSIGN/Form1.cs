using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace daa_ASSIGN
{
    public partial class daa : Form
    {
        public daa()
        {
            InitializeComponent();
        }

        private void daa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int s = Partition(arr, left, right);
                QuickSort(arr, left, s - 1);
                QuickSort(arr, s + 1, right);
            }
        }
        static int Partition(int[] arr, int first, int last)
        {
            int pivotvalue = arr[first];
            int leftmark = first + 1;
            int rightmark = last;
            Boolean done = false;
            while (!done)
            {
                while ((leftmark <= rightmark) && (arr[leftmark] <= pivotvalue))
                    leftmark = leftmark + 1;
                while ((arr[rightmark] >= pivotvalue) && (rightmark >= leftmark))
                    rightmark = rightmark - 1;
                if (rightmark < leftmark)
                    done = true;
                else
                    swap(ref arr[leftmark], ref arr[rightmark]);
            }
            swap(ref arr[first], ref  arr[rightmark]);
            return rightmark;
        }
        static void swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }
        static void HeapSort(int[] input)
        {
            //Build-Max-Heap
            int heapSize = input.Length;
            for (int p = (heapSize - 1) / 2; p >= 0; p--)
                MaxHeapify(input, heapSize, p);
            for (int i = input.Length - 1; i > 0; i--)
            {
                //Swap
                swap(ref input[i], ref input[0]);
                heapSize--;
                MaxHeapify(input, heapSize, 0);
            }
        }
        static void MaxHeapify(int[] input, int heapSize, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;
            if (left < heapSize && input[left] > input[index])
                largest = left;
            else
                largest = index;

            if (right < heapSize && input[right] > input[largest])
                largest = right;

            if (largest != index)
            {
                swap(ref input[index], ref input[largest]);
                MaxHeapify(input, heapSize, largest);
            }
        }

        private void draw_Click(object sender, EventArgs e)
        {

            int size = 100;
            Stopwatch sw = new Stopwatch();
            Random rnd = new Random();
            int[] arr;
            int[] arr1;
            int x;
            int s1 = 4;
            double[] QuickTime = new double[s1+1];
            double[] HeapTime = new double[s1+1];
            int[] xAxis = new int[s1+1];
            for (int i = 0; i <= s1; i++)
            {
                xAxis[i] = size;
                arr = new int[size];
                arr1 = new int[size];
                for (int j = 0; j < size; j++)
                {
                    x = rnd.Next(1, size);
                    arr[j] = x;
                    arr1[j] = x;
                }
                sw.Reset();
                sw.Start();
                QuickSort(arr, 0, size - 1);
                sw.Stop();
                QuickTime[i] = sw.Elapsed.TotalMilliseconds;
                sw.Reset();
                sw.Start();
                HeapSort(arr1);
                sw.Stop();
                HeapTime[i] = sw.Elapsed.TotalMilliseconds;
                size *= 10;
            }
            fillChart(QuickTime, HeapTime, xAxis);
        }
        void fillChart(double [] quick, double [] heap, int[] size)
        {
            chart.Titles.Add("Time Chart").Font = new Font("Arial", 16, FontStyle.Bold);
            chart.ChartAreas[0].AxisX.Title = "Input";
            chart.ChartAreas[0].AxisY.Title = "MilliSeconds";
            chart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 16,FontStyle.Bold);
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 16, FontStyle.Bold);
            chart.Series["QuickSort"].BorderWidth = 4;
            chart.Series["HeapSort"].BorderWidth = 4;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            for (int i = 0; i < heap.Length; i++)
            {
                MessageBox.Show(size[i].ToString()+"  "+quick[i]+"  "+heap[i]);

                //chart.Series["QuickSort"].Points.AddXY(size[i], quick[i]);
                //chart.Series["HeapSort"].Points.AddXY(size[i], heap[i]);
            }
        }
       

    }
}
