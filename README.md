-- Login --

* Proje Login sayfası ile açılmaktadır. Bu sayfa login authentication ile çalışmaktadır.
* Veritabanında yer alan user tablosunda admin kullanıcı oluşturulmuştur, şifresi tabloda gözükmektedir.
* Database connection'ı appsettings.json içerisinde local olarak tanımlıdır.

-- Sipariş Ekranı --
* Login yapıldıktan sonra sipariş ekranı karşımıza gelmektedir.
* Müşteri Numarası alanı "Ara" butonu ile customer tablosunda eğer o no'ya sahipse Müşteri Adıve Soyadı bilgilerini doldurur.
* İşçilik kalemlerinde adet ve indirim alanları on change fonksiyonu ile "Total Fiyat" alanını değiştirmektedir.
* İşçilik kalemi ekle butonu girilen ürün bilgilerini ekleyerek tablo haline getirir.
* Kaydet butonunda basıldığında Müşteri tanımlı değilse yeni müşteri ekler ve siparişlerini de o müşteri üzerinden ekler.
* Diğer durumda var olan müşteriye siparişleri ekler.

-- Listeleme Ekranı --
* Toplamda 3 filtreden oluşur:
* Müşterinin adı ya da soyadı, sipariş numarası ve sipariş tarih aralığı
* Filtreler tekli ya da birden fazla alan girilmesi durumunda verilen koşullara göre listeyi oluşturur.

-- Veritabanı --

* Veritabanında 4 adet tablo bulunmaktadır.
* account : Login işlemi gerçekleştirebilmek için oluşturulan kullanıcı tablosudur.
* customer : Müşteri bilgilerinin olduğu tablodur.
* product : Ürün bilgilerinin olduğu tablodur.
* warehouse : Depo bilgilerinin olduğu tablodur.
* orders : Yapılan siparişlerin bilgisinin tutulduğu tablodur.
