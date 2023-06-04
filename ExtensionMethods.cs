public static class ExtensionMethods
{
    public static void Swap(this int[] arr, int i, int j)
    {
        if (i != j)
        {
            arr[i] ^= arr[j];
            arr[j] ^= arr[i];
            arr[i] ^= arr[j];
        }
    }
}

