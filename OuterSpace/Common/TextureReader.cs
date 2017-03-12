﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OuterSpace.Common
{
    public class TextureReader
    {
        public BitmapImage BitmapFromFile(string path)
        {
            return BytestoBitmap(GetFileBytes(path));
        }

        private BitmapImage BytestoBitmap(byte[] bytes)
        {
            using (MemoryStream byteStream = new MemoryStream(bytes))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = byteStream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private StreamReader GetFileStream(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            return new StreamReader(fileStream);
        }

        private byte[] GetFileBytes(string path)
        {
            using (StreamReader streamReader = GetFileStream(path))
            {
                byte[] bytes = new byte[streamReader.BaseStream.Length];
                streamReader.BaseStream.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }
    }
}