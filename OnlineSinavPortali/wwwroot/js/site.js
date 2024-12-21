// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function eventHandler() {
    var selectedClass = $("#UniversityDepartment").val();
    var subjectSelect = $("#CourseName");
    if (selectedClass == "Elektrik Elektronik") {
        subjectSelect.empty();
        subjectSelect.append('<option value="Elektrik Devreleri">Elektrik Devreleri</option>');
        subjectSelect.append('<option value="Matematik">Matematik </option>');
        subjectSelect.append('<option value="Fizik">Fizik</option>');
        subjectSelect.append('<option value="Sayısal Devirler">Sayısal Devirler</option>');
        subjectSelect.append('<option value="Diferansiyel Denklemler">Diferansiyel Denklemler</option>');

    }
    else if (selectedClass == "Harita Kadastro") {
        subjectSelect.empty();
        subjectSelect.append('<option value="Topografya">Topografya</option>');
        subjectSelect.append('<option value="Harita Trigonometrisi">Harita Trigonometrisi</option>');
        subjectSelect.append('<option value="Harita Bilgisi">Harita Bilgisi</option>');
        subjectSelect.append('<option value="Harita Çizimi">Harita Çizimi</option>');
        subjectSelect.append('<option value="Arazi Yönetimi">Arazi Yönetimi</option>');
    }
    else {

        subjectSelect.empty();
        subjectSelect.append('<option value="Matematik">Matematik</option>');
        subjectSelect.append('<option value="Türkçe">Türkçe</option>');
        subjectSelect.append('<option value="Programlamanın Temelleri">Programlama Temelleri</option>');
        subjectSelect.append('<option value="İnternet Programcılığı">İnternet Programcılığı</option>');
        subjectSelect.append('<option value="Veri Tabanı">Veri Tabanı</option>');
        subjectSelect.append('<option value="Multimedya Uygulamaları">Multimedya Uygulamaları</option>');
        subjectSelect.append(' <option value="Nesne Tabanlı Programlama">Nesne Tabanlı Programlama</option>');
        subjectSelect.append('<option value="İleri Programlama">İleri Programlama</option>');


    }
}
$("#UniversityDepartment").change(eventHandler);