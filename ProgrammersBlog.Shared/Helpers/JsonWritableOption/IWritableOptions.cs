﻿using Microsoft.Extensions.Options;
using System;

namespace ProgrammersBlog.Shared.Helpers.JsonWritableOption
{
    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        void Update(Action<T> applyChanges); // (x=>x.Header = "Yeni Başlık") x=>{x.Header = "Yeni Başlık";x.Content="Yeni İçerik"}
    }
}
