﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Hashing
{
    class HashingSupervisor
    {
        ///// <summary>
        ///// This method returns hmacHashed random generated byte array
        ///// </summary>
        ///// <param name="Type"></param>
        ///// <returns></returns>
        //public byte[] RandomKeyGenerator(string userInputKey)
        //{
        //    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        //    RNGCryptoServiceProvider eng = new RNGCryptoServiceProvider();
        //    byte[] input = new byte[32];
        //    eng.GetBytes(input);
        //    return input;
        //    //byte[] bytes = Encoding.UTF8.GetBytes(userInputKey);
        //}


        /// <summary>
        /// Returns the string as hmacHashed byte array
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public byte[] GetBytes(string userInput)
        {
            return Encoding.UTF8.GetBytes(userInput);
        }

        /// <summary>
        /// This method returns hmacHashed string with the key in Base64String
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetBytesToPlainText(byte[] key)
        {
            MessageBox.Show($"This is hmacHashed base64 String: {Convert.ToBase64String(key)}");
            return BitConverter.ToString(key).Replace("-", String.Empty);
            //return Convert.ToBase64String(key);
        }

        public string GetByteArrayToHexString(byte[] input)
        {
            return BitConverter.ToString(input);
        }

        /// <summary>
        /// This method returns hmacHashed Hashed string with the key in MD5
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] MD5Hashed(byte[] key)
        {
            using MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(key);
        }

        /// <summary>
        /// This method returns hmacHashed ashed string with the key in SHA1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] SHA1Hashing(byte[] key)
        {
            using SHA1Managed sha1 = new SHA1Managed();
            return sha1.ComputeHash(key);
        }

        /// <summary>
        /// This method returns hmacHashed Hashed string with the key in SHA256
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] SHA256Hashing(byte[] key)
        {
            using SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] GenerateHmac256Message(byte[] key, byte[] message)
        {
            using HMACSHA256 hmac = new HMACSHA256(key);
            byte[] hashValue = hmac.ComputeHash(message);
            return hashValue;
        }

        /// <summary>
        /// Checks for authentication on hash and message
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hmacHashed"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CheckAuthenticity(byte[] key, byte[] hmacHashed, string message)
        {
            using HMACSHA256 hmac256 = new HMACSHA256(key);
            int length = hmac256.HashSize / 8;
            byte[] computedHash = hmac256.ComputeHash(GetBytes(message));
            return CompareByteArrays(hmacHashed, computedHash, length);
        }

        /// <summary>
        /// This method check for every bite tha it is the same value
        /// </summary>
        /// <param name="hmacHashed"></param>
        /// <param name="computedHash"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private bool CompareByteArrays(byte[] hmacHashed, byte[] computedHash, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (hmacHashed[i] != computedHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// This method returns hmacHashed Hashed string with the key in SHA512
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] SHA512Hashing(byte[] key)
        {
            using SHA512Managed sha512 = new SHA512Managed();
            return sha512.ComputeHash(key);
        }

        public byte[] GetHashedKey(int algorithm, byte[] key)
        {
            switch (algorithm)
            {
                case 0:
                    return MD5Hashed(key);

                case 1:
                    return SHA1Hashing(key);

                case 2:
                    return SHA256Hashing(key);

                case 3:
                    return SHA512Hashing(key);
            }

            return new byte[] { };
        }
    }
}