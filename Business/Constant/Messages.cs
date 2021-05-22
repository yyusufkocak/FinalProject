using Core.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constant
{
    public static class Messages
    {
        public static string successTransaction = "İşlem Başarılı";
        public static string ErrorTransaction = "İşlem Başarılı Değil";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz";
        public static string ProductUpdated = "Ürün Güncellendi";
        public static string ProductDeleted="Ürün Silindi";


        public static string CategoryNameInvalid="Kategori İsmi Geçersiz";
        public static string CategoryAdded = "Kategori Eklendi";
        public static string CategoryDeleted = "Kategori Silindi";
        public static string CategoryUpdated = "Kategori Güncellendi";
        public static string CategoryAlreadyExists = "Böyle Bir Kategori Zaten Var";


        public static string OrderAdded ="Sipariş Eklendi";
        public static string OrderDeleted ="Sipariş Silindi";
        public static string OrderUpdated ="Sipariş Güncellendi";
        public static string OrderLimit = "Günlük Sipariş Limiti Aşıldı ";


        public static string AuthorizationDenied = "Yetkilendirme Hatası";
        public static string PriceError= "Girdiğiniz Değer Ürünün Piyasa Fiyatı İle Aynı Olmalıdır";
        public static string UserRegistered = "Kullanıcı Kaydı Başarılı";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Hatalı Şifre";
        public static string SuccessLogin = "Başarılı Giriş";
        public static string UserAlreadyExists = "Kullanıcı zaten var";
        public static string PasswordNotStrongEnough = "Şifre Yeterince Güçlü Değil";

    }
}
