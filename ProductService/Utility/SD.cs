﻿namespace ProductService.Utility
{
    public class SD
    {
        public static string ProductApiBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
