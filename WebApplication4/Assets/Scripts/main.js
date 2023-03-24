// Kategori Ekleme
$(document).on("click", "#kategoriEkle", async function () {
    const { value: formValues } = await Swal.fire({
        title: 'Kategori Ekle',
        html:
            '<input id="ctgName" class="swal2-input">'
    })

    var ctgName = $("#ctgName").val();
    $.ajax({
        type: 'Post',
        url: '/Category/AddJson',
        data: { "ctgName": ctgName },
        dataType: 'Json',
        success: function (data) {
            var ctgId = data.Result.Id;
            var ctgName = '<td>' + data.Result.CategoryName + '</td>';
            var updateBtn = "<td><button class='update btn btn-success' value='" + ctgId + "'>Güncelle</button></td>";
            var deleteBtn = "<td><button class='delete btn btn-danger' value='" + ctgId + "'>Sil</button></td>";
            var BookPiece = "<td>0</td>";
            $("#example tbody").prepend('<tr>' + ctgName + BookPiece + updateBtn + deleteBtn + '</tr>');
            Swal.fire({
                type: 'succes',
                title: 'Kategori Eklendi',
                text: 'İşlem başarıyla gerçekleşti!'
            });
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Kategori Eklenemedi',
                text: 'Beklenmeyen bir hata oluştu'
            });
        }
    });
});

// Kategori İsmini Güncelleme
$(document).on("click", ".update", async function () {
    var ctgId = $(this).val();
    var ctgAdTd = $(this).parent("td").parent("tr").find("td:first");
    var CategoryName = ctgAdTd.html();
    const { value: formValues } = await Swal.fire({
        title: 'Kategori Güncelle',
        html:
            '<input id="ctgName" value="' + CategoryName +'"class=swal2-input">'
    })
    CategoryName = $("#ctgName").val();
    if (formValues) {
        $.ajax({
            type: 'Post',
            url: '/Category/UpdateJson',
            data: {"ctgId": ctgId, "ctgCategoryName": CategoryName },
            dataType: 'Json',
            success: function (data) {
                if (data == "1") {
                    ctgAdTd.html(CategoryName);
                    Swal.fire({
                        type: 'succes',
                        title: 'Kategori Güncellendi',
                        text: 'İşlem Başarıyla Gerçekleşti'
                    });
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: 'Kategori Güncellenemedi',
                        text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    type: 'error',
                    title: 'Kategori Eklenmedi',
                    text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                });
            }
        });
    }
});

// Kategoriyi Silme
$(document).on("click", ".delete", async function () {
    var tr = $(this).parent("td").parent("tr");
    var ctgId = $(this).val();
    $.ajax({
        type: 'Post',
        url: '/Category/DeleteJson',
        data: { "ctgId": ctgId },
        dataType: 'Json',
        success: function (data) {
            if (data == "1") {
                tr.remove();
                Swal.fire({
                    type: 'succes',
                    title: 'Kategori Silindi!',
                    text: 'İşlem Başarıyla Gerçekleşti!'
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Kategori Silinemedi!',
                    text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Kategori Silinemedi!',
                text: 'Beklenmeyen bir hata ile karşılaşıldı!'
            });
        }
    });
});

// Yazar Ekleme
$(document).on("click", "#yazarEkle", async function () {
    const { value: formValues } = await Swal.fire({
        title: 'Yazar Ekle',
        html:
            '<input id="wrtName" class="swal2-input">'
    })

    var wrtName = $("#wrtName").val();
    $.ajax({
        type: 'Post',
        url: '/Writer/AddJson',
        data: { "wrtName": wrtName },
        dataType: 'Json',
        success: function (data) {
            var wrtId = data.Result.Id;
            var wrtName = '<td>' + data.Result.Name + '</td>';
            var updateBtn = "<td><button class='update btn btn-success' value='" + wrtId + "'>Güncelle</button></td>";
            var deleteBtn = "<td><button class='delete btn btn-danger' value='" + wrtId + "'>Sil</button></td>";
            var BookPiece = "<td>0</td>";
            $("#example tbody").prepend('<tr>' + wrtName + BookPiece + updateBtn + deleteBtn + '</tr>');
            Swal.fire({
                type: 'succes',
                title: 'Yazar Eklendi',
                text: 'İşlem başarıyla gerçekleşti!'
            });
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Yazar Eklenemedi',
                text: 'Beklenmeyen bir hata oluştu'
            });
        }
    });
});

// Yazar Silme
$(document).on("click", ".delete-wrt", async function () {
    var tr = $(this).parent("td").parent("tr");
    var wrtId = $(this).val();
    $.ajax({
        type: 'Post',
        url: '/Writer/DeleteJson',
        data: { "wrtId": wrtId },
        dataType: 'Json',
        success: function (data) {
            if (data == "1") {
                tr.remove();
                Swal.fire({
                    type: 'succes',
                    title: 'Yazar Silindi!',
                    text: 'İşlem Başarıyla Gerçekleşti!'
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Yazar Silinemedi!',
                    text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Yazar Silinemedi!',
                text: 'Beklenmeyen bir hata ile karşılaşıldı!'
            });
        }
    });
});

// Yazar Güncelleme
$(document).on("click", ".update-wrt", async function () {
    var wrtId = $(this).val();
    var wrtAdTd = $(this).parent("td").parent("tr").find("td:first");
    var wrtName = wrtAdTd.html();
    const { value: formValues } = await Swal.fire({
        title: 'Yazar Güncelle',
        html:
            '<input id="wrtName" value="' + wrtName + '"class=swal2-input">'
    })
    wrtName = $("#wrtName").val();
    if (formValues) {
        $.ajax({
            type: 'Post',
            url: '/Writer/UpdateJson',
            data: { "wrtId": wrtId, "wrtName": wrtName },
            dataType: 'Json',
            success: function (data) {
                if (data == "1") {
                    wrtAdTd.html(wrtName);
                    Swal.fire({
                        type: 'succes',
                        title: 'Yazar Güncellendi',
                        text: 'İşlem Başarıyla Gerçekleşti'
                    });
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: 'Yazar Güncellenemedi',
                        text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    type: 'error',
                    title: 'Yazar Eklenmedi',
                    text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                });
            }
        });
    }
});

// Kategori Seçme İşlemi
$(document).on("click", "#addCategory", function () {
    var selectedCategory = $("#categories").val();
    if (selectedCategory != null && selectedCategory != "") {
        var selectedId = $("#categories option:selected").attr("data-id");
        $("#addedCategories").append('<div id="' + selectedId + '" class="col-2 ctg-text" style="margin-right:5px;">' + selectedCategory + '</div>');
        $("#categories option:selected").remove();
    }
});

// Seçilen Kategoriyi Geri Alma
$(document).on("click", ".ctg-text", function () {
    var id = $(this).attr("id");
    var categoryName = $(this).html();
    $("#categories").append('<option data-id="' + id + '">' + categoryName + '</option>');
    $(this).remove();
});

// Kitap Ekleme İşlemi
$(document).on("click", "#saveBook", function () {
    var values = {
        categories: [],
        writer : $("#writers option:selected").attr("data-id"),
        bookName : $("#bookName").val(),
        bookPiece : $("#bookPiece").val()
    };

    $("#addedCategories div").each(function () {
        var id = $(this).attr("id");
        values.categories.push(id);
    });

    $.ajax({
        type: 'Post',
        url: '/Books/EkleJson',
        data: JSON.stringify(values),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (getValues) {
            if (getValues == "0") {
                Swal.fire({
                    type: 'succes',
                    title: 'Kitap Eklendi',
                    text: 'İşlem Başarıyla Gerçekleşti!'
                });
            }
            else if ("cannotNull") {
                Swal.fire({
                    type: 'error',
                    title: 'Eksik Veri Girişi!',
                    text: 'Lütfen tüm alanları doldurunuz!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Kitap Eklenmedi',
                text: '----Beklenmeyen bir hata ile karşılaşıldı!'
            });
        }
    });

});

// Kitap Silme İşlemi
$(document).on("click", ".delete-books", function () {
    Swal.fire({
        title: 'Kitap Silinecek Emin Misiniz?',
        text: "Bu işlem geri alınamaz!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Sil',
        cancelButtonText:'Vazgeç'
    }).then((result) => {
        if (result.value) {
            var booksId = $(this).val();
            var tr = $(this).parent("td").parent("tr");

            $.ajax({
                type: 'Post',
                url: '/Books/DeleteJson',
                data: { "booksId": booksId },
                dataType: 'Json',
                success: function (data) {
                    if (data == "1") {
                        tr.remove();
                        Swal.fire({
                            type: 'succes',
                            title: 'Kitap Silindi',
                            text: 'İşlem Başarıyla Gerçekleşti!'
                        });
                    }
                    else {
                        Swal.fire({
                            type: 'error',
                            title: 'Kitap Silinemedi',
                            text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                        });
                    }
                },
                error: function(){
                    Swal.fire({
                        type: 'error',
                        title: 'Kitap Silinemedi',
                        text: 'Beklenmeyen bir hata ile karşılaşıldı!!!'
                    });
                }
            });
        }
    })

});

// Kitap Güncelleme İşlemi -----KONTROL ETTTTTT!!!
$(document).on("click", "#updateBook", function () {
    var values = {
        categories: [],
        writer: $("#writers option:selected").attr("data-id"),
        bookName: $("#bookName").val(),
        bookPiece: $("#bookPiece").val(),
        booksId: $(this).attr("data-id")
    };

    $("#addedCategories div").each(function () {
        var id = $(this).attr("id");
        values.categories.push(id);
    });

    $.ajax({
        type: 'Post',
        url: '/Books/UpdateJson',
        data: JSON.stringify(values),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (getValues) {
            if (getValues == "1") {
                Swal.fire({
                    type: 'succes',
                    title: 'Kitap Güncellendi',
                    text: 'İşlem Başarıyla Gerçekleşti!'
                });
            }
            else if ("cannotNull") {
                Swal.fire({
                    type: 'error',
                    title: 'Eksik Veri Girişi!',
                    text: 'Lütfen tüm alanları doldurunuz!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Kitap Güncellenemedi',
                text: 'Beklenmeyen bir hata ile karşılaşıldı!'
            });
        }
    });
});

//Üye Kaydetme
$(document).on("click", "#saveMember", function () {
    var values = {
        memberName: $("#memberName").val(),
        memberLastName: $("#memberLastName").val(),
        memberTCKNO: $("#memberTCKNO").val(),
        memberPhone: $("#memberPhone").val()
    };

    $.ajax({
        type: 'Post',
        url: '/Members/AddJson',
        data: JSON.stringify(values),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (getValues) {
            if (getValues == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Üye Eklendi',
                    text: 'İşlem Başarıyla Gerçekleşti!'
                });
            }
            else if ("cannotNull") {
                Swal.fire({
                    type: 'error',
                    title: 'Üye Eklenmedi!',
                    text: 'Lütfen Boş Alanları Kontrol Edin!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Üye Eklenmedi!',
                text: 'Beklenmeyen Bir Hata Oluştu!'
            });
        }
    });

});

//Üye Silme 
$(document).on("click", ".delete-member", function () {
    Swal.fire({
        title: 'Üye Kaydı Silinecek Emin Misiniz?',
        text: "Bu işlem geri alınamaz!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Sil',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.value) {
            var memberID = $(this).val();
            var tr = $(this).parent("td").parent("tr");

            $.ajax({
                type: 'Post',
                url: '/Members/DeleteJson',
                data: { "memberID": memberID },
                dataType: 'Json',
                success: function (data) {
                    if (data == "1") {
                        tr.remove();
                        Swal.fire({
                            type: 'succes',
                            title: 'Üye Kaydı Silindi',
                            text: 'İşlem Başarıyla Gerçekleşti!'
                        });
                    }
                    else {
                        Swal.fire({
                            type: 'error',
                            title: 'Üye Kaydı Silinemedi',
                            text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        type: 'error',
                        title: 'Üye Kaydı Silinemedi',
                        text: 'Beklenmeyen bir hata ile karşılaşıldı!!!'
                    });
                }
            });
        }
    })

});

//Üye Güncelleme
$(document).on("click", "#updateMember", function () {
    var values = {
        memberName: $("#memberName").val(),
        memberLastName: $("#memberLastName").val(),
        memberTCKNO: $("#memberTCKNO").val(),
        memberPhone: $("#memberPhone").val(),
        memberID: $(this).attr("data-id")
    };

    $.ajax({
        type: 'Post',
        url: '/Members/UpdateJson',
        data: JSON.stringify(values),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (getValues) {
            if (getValues == "1") {
                Swal.fire({
                    type: 'succes',
                    title: 'Üye Kaydıo Güncellendi',
                    text: 'İşlem Başarıyla Gerçekleşti!'
                });
            }
            else if ("cannotNull") {
                Swal.fire({
                    type: 'error',
                    title: 'Eksik Veri Girişi!',
                    text: 'Lütfen tüm alanları doldurunuz!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Üye Kaydı Güncellenemedi',
                text: 'Beklenmeyen bir hata ile karşılaşıldı!'
            });
        }
    });
});

//Kitap Verme
$(document).on("click", "#giveBook", function () {
    var values = {
        memberId: $("#members option:selected").attr("data-id"),
        bookId: $("#books option:selected").attr("data-id"),
        deliveryDate: $("#DeliveryDate").val(),
    };

    $.ajax({
        type: 'Post',
        url: '/GivenBooks/GiveBooksJson',
        data: JSON.stringify(values),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (getValues) {
            if (getValues == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Kitap Verildi!',
                    text: 'İşlem Başarıyla Gerçekleşti!'
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Kitap Verilemedi!',
                    text: 'Verilirken Bir Sorun İle Karşılaşıldı!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Kitap Verilemedi!',
                text: 'Beklenmeyen Bir Hata Oluştu!'
            });
        }
    });

});

// Verilen Kitabı Güncelle
$(document).on("click", "#givenBooksUpdate", function () {
    var values = {
        GivenBooksID: $(this).attr("data-id"),
        memberId: $("#members option:selected").attr("data-id"),
        bookId: $("#books option:selected").attr("data-id"),
        deliveryDate: $("#DeliveryDate").val(),
    };

    $.ajax({
        type: 'Post',
        url: '/GivenBooks/UpdateGivenBooksJson',
        data: JSON.stringify(values),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (getValues) {
            if (getValues == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Bilgiler Güncellendi!',
                    text: 'İşlem Başarıyla Gerçekleşti!'
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Bilgiler Güncellenemedi!',
                    text: 'Güncellenirken Bir Sorun İle Karşılaşıldı!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Bilgiler Güncellenemedi!',
                text: 'Beklenmeyen Bir Hata Oluştu!'
            });
        }
    });

});

//Kitap Teslim Alma İşlemi
$(document).on("click", ".deliveredBtn", function () {
    Swal.fire({
        title: 'Teslim Alınıyor...',
        text: "Teslim Alındı Olarak İşaretlensin Mi?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.value) {
            var givenbooksId = $(this).val();
            var tr = $(this).parent("td").parent("tr");
            $.ajax({
                type: 'Post',
                url: '/GivenBooks/TakeJson',
                data: { "givenbooksId": givenbooksId },
                dataType: 'Json',
                success: function (data) {
                    if (data == "1") {
                        tr.remove();
                        Swal.fire({
                            type: 'succes',
                            title: 'Kitap Teslim Alındı!',
                            text: 'İşlem Başarıyla Gerçekleşti!'
                        });
                    }
                    else {
                        Swal.fire({
                            type: 'error',
                            title: 'Kitap Teslim Alınırken Bir Hata Oluştu!',
                            text: 'Beklenmeyen bir hata ile karşılaşıldı!'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        type: 'error',
                        title: 'Kitap Teslim Alınamadı!',
                        text: 'Beklenmeyen bir hata ile karşılaşıldı!!!'
                    });
                }
            });
        }
    })

});

//Admin Girişi
$(document).on("click", "#loginBtn", function () {
    $(this).html("Wait...");
    $(this).prop("disabled", true);

    var values = {
        mail: $("#mail").val(),
        password: $("#password").val(),
        remember: false
    };

    if ($("#cbRemember").is(":checked")) {
        values.remember = true;
    }

    $.ajax({
        type: 'Post',
        url: '/Login/LoginControl',
        data: JSON.stringify(values),
        dataType: 'Json',
        contentType: 'application/json;charset=utf-8',
        success: function (getValues) {
            if (getValues == "Success") {
                Swal.fire({
                    icon: 'success',
                    title: 'Giriş Başarılı!',
                    text: 'Yönlendiriliyorsunuz...'
                });
                window.location.href = '/Books/Index';
            }
            else if (getValues == "cannotNull") {
                Swal.fire({
                    icon: 'error',
                    title: 'Giriş Başarısız!',
                    text: 'Lütfen Boş Alanları Tekrar Kontrol Edin!'
                });
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Giriş Başarısız!',
                    text: 'Lütfen Bilgileri Tekrar Kontrol Edin!'
                });
            }
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Giriş Başarısız!',
                text: 'Beklenmeyen bir hata oluştu!'
            });
        },
        complete: function () {
            $("#loginBtn").html("Login");
            $("#loginBtn").prop("disabled", false);
        }
    });
});


