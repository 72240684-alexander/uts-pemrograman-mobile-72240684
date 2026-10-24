# uts-pemrograman-mobile-72240684
# Sistem Informasi Akademik

Aplikasi Sistem Informasi Akademik merupakan aplikasi untuk mengelola data mahasiswa dan dosen. Aplikasi ini dikembangkan menggunakan ASP.NET Core Web API sebagai backend dan .NET MAUI Blazor Hybrid sebagai frontend. Database yang digunakan adalah SQLite, sedangkan Entity Framework Core digunakan untuk mengelola akses data dan perubahan struktur database.

## 1. Fitur Aplikasi

Fitur yang tersedia pada aplikasi meliputi:

- Menampilkan data mahasiswa.
- Menambahkan data mahasiswa.
- Mengubah data mahasiswa.
- Menghapus data mahasiswa.
- Menampilkan data dosen.
- Menambahkan data dosen.
- Mengubah data dosen.
- Menghapus data dosen.
- Mencari data berdasarkan fitur pencarian yang tersedia pada halaman.
- Mengakses endpoint API melalui Swagger untuk pengujian manual.

Fitur yang dapat digunakan bergantung pada implementasi yang tersedia di dalam source code.

## 2. Teknologi yang Digunakan

- **C#** sebagai bahasa pemrograman.
- **.NET 8** sebagai platform pengembangan.
- **ASP.NET Core Web API** untuk backend.
- **.NET MAUI Blazor Hybrid** untuk frontend.
- **Entity Framework Core** untuk akses data dan migration.
- **SQLite** sebagai database.
- **Swagger / OpenAPI** untuk dokumentasi dan pengujian API.
- **Visual Studio 2022** sebagai lingkungan pengembangan.

## 3. Struktur Proyek

SistemInformasiAkademik/
├── AcademicAPI/
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Migrations/
│   ├── Models/
│   ├── Program.cs
│   ├── appsettings.json
│   └── AcademicAPI.csproj
├── AcademicHybridApp/
│   ├── Components/
│   ├── Platforms/
│   ├── Resources/
│   ├── wwwroot/
│   ├── MauiProgram.cs
│   └── AcademicHybridApp.csproj
├── SistemInformasiAkademik.sln
└── README.md

## 4. Prasyarat

Sebelum menjalankan aplikasi, pastikan perangkat sudah memiliki:

- Windows untuk menjalankan lingkungan pengembangan yang digunakan.
- Visual Studio 2022 dengan workload ASP.NET and web development.
- Workload .NET MAUI untuk menjalankan aplikasi frontend.
- .NET SDK yang sesuai dengan target framework proyek.
- Koneksi internet saat mengunduh dependency yang diperlukan pertama kali.

## 5. Cara Mendapatkan Proyek

Clone repository dari GitHub menggunakan terminal:

```bash
git clone https://github.com/72240684-alexander/uts-pemrograman-mobile-72240684.git
```

Masuk ke folder proyek:

```bash
cd uts-pemrograman-mobile-72240684
```

Buka file `SistemInformasiAkademik.sln` menggunakan Visual Studio 2022.

## 6. Konfigurasi Database

Backend menggunakan SQLite dengan file database `akademik.db`. Konfigurasi koneksi database dapat diperiksa pada file `AcademicAPI/appsettings.json` dan konfigurasi `AcademicDbContext`.

Pastikan connection string menunjuk ke lokasi database yang benar. Jika menggunakan lokasi relatif, pastikan aplikasi dijalankan dengan konfigurasi yang sesuai agar file database yang digunakan tidak berbeda dari yang diharapkan.

### Menyiapkan struktur database

Proyek menyediakan file EF Core Migration di folder `AcademicAPI/Migrations/`.

Jika database perlu dibuat atau diperbarui menggunakan migration, pastikan Entity Framework Core CLI dan paket desain yang diperlukan telah tersedia. Kemudian, jalankan perintah berikut dari folder `AcademicAPI`:

```bash
dotnet ef database update
```

Perintah tersebut menerapkan migration ke database sesuai konfigurasi proyek.

**Catatan:** periksa connection string sebelum menjalankan migration. Jika file database yang sudah ada berisi data penting, buat salinan cadangan terlebih dahulu.

## 7. Menjalankan Backend API

Buka terminal di folder utama repository, kemudian jalankan:

```bash
cd AcademicAPI
dotnet restore
dotnet run
```

Periksa alamat dan port yang ditampilkan pada terminal. Berdasarkan konfigurasi pengembangan, API digunakan pada alamat berikut:

```text
http://localhost:5017/
```

Dokumentasi Swagger dapat dibuka melalui:

```text
http://localhost:5017/swagger
```

Jika Swagger tidak tersedia pada alamat tersebut, periksa output terminal dan konfigurasi `launchSettings.json` untuk mengetahui alamat yang benar.

## 8. Menjalankan Frontend

Frontend menggunakan .NET MAUI Blazor Hybrid.

1. Buka `SistemInformasiAkademik.sln` di Visual Studio 2022.
2. Pastikan backend `AcademicAPI` sudah berjalan.
3. Pilih `AcademicHybridApp` sebagai startup project.
4. Pilih target perangkat yang tersedia, misalnya Windows Machine.
5. Jalankan aplikasi menggunakan tombol **Run** atau tekan **F5**.

Pastikan alamat API pada konfigurasi frontend sesuai dengan alamat backend yang sedang berjalan.

Untuk aplikasi yang dijalankan pada perangkat Android atau perangkat lain, alamat `localhost` mungkin perlu diganti dengan alamat host yang dapat dijangkau oleh perangkat tersebut.

## 9. Pengujian API dengan Swagger

Swagger dapat digunakan untuk menguji endpoint backend secara manual.

Langkah-langkah:

1. Jalankan `AcademicAPI`.
2. Buka halaman Swagger.
3. Pilih endpoint mahasiswa atau dosen.
4. Tekan **Try it out**.
5. Masukkan data yang dibutuhkan jika endpoint memerlukan input.
6. Tekan **Execute**.
7. Periksa status HTTP dan respons yang diberikan API.

Pengujian ini membantu memastikan endpoint memberikan respons sesuai dengan permintaan yang dikirimkan.

## 10. Data dan Akun Pengujian

Aplikasi ini berfokus pada pengelolaan data mahasiswa dan dosen. Data pengujian dapat menggunakan data contoh yang tidak mengandung informasi pribadi.

Tidak ada akun login khusus yang didokumentasikan dalam README ini. Jika implementasi aplikasi menggunakan autentikasi, informasi akun uji perlu ditambahkan sesuai konfigurasi yang sebenarnya.

## 11. Pemecahan Masalah

### API tidak dapat diakses

- Pastikan backend sedang berjalan.
- Periksa alamat dan port API pada terminal.
- Pastikan alamat API di frontend sesuai dengan alamat backend.

### Database tidak ditemukan atau tabel belum tersedia

- Periksa connection string pada `appsettings.json`.
- Pastikan file database berada di lokasi yang sesuai.
- Periksa apakah migration telah diterapkan.

### Dependency belum tersedia

Jalankan perintah berikut pada folder proyek yang sesuai:

```bash
dotnet restore
```

Jika masalah tetap muncul, periksa versi .NET SDK dan target framework pada file `.csproj`.

## 12. Repository

Source code proyek tersedia di GitHub:

https://github.com/72240684-alexander/uts-pemrograman-mobile-72240684
