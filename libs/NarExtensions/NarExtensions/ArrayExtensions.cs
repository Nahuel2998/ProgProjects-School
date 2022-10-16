using System.Text;

namespace NarExtensions
{
    // yeah most of this comes from stackoverflow more news at nine
    public static class ArrayExtensions
    {
        // TODO: Add startIndex and endIndex to this method
        public static void Fill<T>(this T[] destinationArray, params T[] values)
        {
            if (destinationArray == null)
            { throw new ArgumentNullException(nameof(destinationArray)); }

            Array.Copy(values, destinationArray, Math.Min(values.Length, destinationArray.Length));

            if (values.Length >= destinationArray.Length)
            { return; }

            int arrayToFillHalfLength = destinationArray.Length / 2;
            int copyLength;

            for (copyLength = values.Length; copyLength < arrayToFillHalfLength; copyLength <<= 1)
            { Array.Copy(destinationArray, 0, destinationArray, copyLength, copyLength); }

            Array.Copy(destinationArray, 0, destinationArray, copyLength, destinationArray.Length - copyLength);
        }

        public static void Fill(this Array array, object value)
        {
            var indices = new int[array.Rank];

            array.Fill(0, indices, value);
        }

        public static void Fill(this Array array, int dimension, int[] indices, object value)
        {
            if (dimension < array.Rank)
            {
                for (int i = array.GetLowerBound(dimension); i <= array.GetUpperBound(dimension); i++)
                {
                    indices[dimension] = i;

                    array.Fill(dimension + 1, indices, value);
                }
            }
            else
            { array.SetValue(value, indices); }
        }

        // Generalize this?
        public static unsafe void SpanFill(this char[,] array, char value)
        {
            fixed (char* a = &array[0, 0])
            { new Span<char>(a, array.GetLength(0) * array.GetLength(1)).Fill(value); }
        }

        public static string ToMatrixString<T>(this T[,] matrix, string delimiter = "")
        {
            var res = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                { res.Append(matrix[i, j]).Append(delimiter); }

                res.AppendLine();
            }

            return res.ToString();
        }
    }
}