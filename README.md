#### *Projede kullanılan teknoloji ve kütüphaneler*
- **Angular** / **Clean Architecture** / **Ef Core**  / **MsSql** / **CQRS** / **MediatR** / **Fluent Validation** / **AutoMapper**  / **JWT Authentication** / **Generic Repository pattern** / **Result pattern** / **UnitOfWork pattern** / **XUnit Test**


![Image](https://github.com/user-attachments/assets/d85fdaf4-1d4b-41ef-b891-f21da4d8d137)

# HRManagementSystem App

### Kullanıcı 
-  __Register__ her Çalişan oluşturuldugunda Onun için bir kullanıcı oluşturuluyor şekilde. ``Email : çalişan email  Password : HumanR1,``
-  Her çalişan açılan kulanıcıdan sonra __Login__ olabiliyor.  Admin User ``Email : admin@admin.com  Password : Admin1,``
-  Kullanıcılara 4 Yetki den biri __Departman__ bilgisine göre ilk kayıt anında atanıyor. __Admin__ - __Employee__ - __HumanResources__ - __Manager__
-  Kulanıcılar Default olarak atanan şifrelerini degiştirebilir.

### Çalişan
- Her Çalişan için bir kulanıcı İK tarafındna oluşturuluyor.
- İsme göre Filtreleme ve pagination yapısı ile listeleniyor çalişanlar.
- Çalişan rolu __Admin__ - __HumanResources__ - __Manager__ biri ise Çalişan Detay sayfasını görebilir. 

 ### İzin Formları
- Çalişanlar izin formları ekleme,güncelleme işlemleri yapabiliyor.
- Her Çalişanın kendi izin formları listeleniyor.
- İzin formları ilk olarak Beklemede daha sonra yönetici tarafından Onay veya Red olarak işleniyor. Çalışan Detay sayfasında yapılıyor.

 ### Maaşlar
- Çalişanların maaşlarını ekleme güncelleme İK Rolu yönetiminde. Çalışan Detay sayfasında yapılıyor.
- Her Çalişanın kendi maaş detayını son aldığı tarihe göre görüntüleyebiliyor.

 ### Performanslar
- Çalişanlar için performans formları ilgili birim tarafından Ekleniyor. Çalışan Detay sayfasında yapılıyor.
- Her Çalişanın kendi performans bilgileri listeleniyor.
- Çalışanların Performansları İK Tarafından Degerlendiriliyor. Çalışan Detay sayfasında yapılıyor.
  

   ## Projenin Kurulumu
 - Projeyi aşagıdaki adresden biligisayarınıza klonlayabilirsiniz
 ````
https://github.com/GuvenBoydak/HRManagementSystem.git
 ````
 - Proje’yi çalıştırmak için Mssql'in bilgisayarımızda yüklü ve çalışıyor olması gerekmektedir. Daha Sonra Presentation dosyası içerisinden Library.Api katmanındaki ``appsettings.json`` içerisindeki ``"ConnectionStrings": {
    "SqlServer": "database adresiniz"
   }`` database adresiniz kısmını kendi database ayaralarınıza göre değiştirmelisiniz.



