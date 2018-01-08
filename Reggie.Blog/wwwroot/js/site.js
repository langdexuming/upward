// Write your Javascript code.
/* 添加String内之类的动态方法
 * 判断字符串是否为空
 */
String.prototype.IsNullOrEmpty = function() {
    if (this === null || this === undefined || this === '' || this.length === 0) {
        return true;
    }
    return false;
}

/*!

获取元素所在内容区的Top位置

*/
function GetPositionTop(element) {
    return element.position().top - $(document).scrollTop();
}

/*!

文档加载--共享页面使用

*/
$(function() {
    var url = document.URL;

    if (url.indexOf("Index") >= 0) {
        $('#liIndex').addClass('active');
    } else if (url.indexOf("GameLife") >= 0) {
        $('#liGameLife').addClass('active');
    } else if (url.indexOf("LeaveMessage") >= 0) {
        $('#liLeaveMessage').addClass('active');
    } else if (url.indexOf("Resume") >= 0) {
        $('#liResume').addClass('active');
    } else {
        $('#liIndex').addClass('active');
    }

});

// $.ajax({
//     url: "Index.cshtml",
//     data: {
//         zipcode: 97201
//     },
//     success: function(result) {
//         $('#navbar li').each(function(i) {
//             if (i === 2) {
//                 if (!$(this).hasClass('active')) {
//                     $(this).addClass('active');
//                 }
//             }
//         });
//     }
// });

/****************************index.cshtml start******************************/
//文档加载时执行
$(function() {

    // var ajaxRequestUrl = "TestApi";

    // if (window.location.href.indexOf("Register") > 0) {
    //     var div = document.getElementById('navbar');
    //     div.style.display = 'none';
    // }

    // $('#btn-sendRequest').on('click', function() {
    //     var url = ajaxRequestUrl;
    //     $(document).load(url);
    // })

    // //响应Ajax

    // $(document).ajaxSend(function(evnet, jqXHR, jqSettingss) {
    //     if (jqSettingss.url == ajaxRequestUrl) {
    //         console.log("ajax requeset error, url is " + jqSettingss.url);
    //     }
    // })

    // $(document).ajaxError(function(evnet, jqXHR, jqSettingss) {
    //     if (jqSettingss.url == ajaxRequestUrl) {
    //         console.log("ajax requeset error, url is " + jqSettingss.url);
    //     }
    // })

    // $(document).ajaxSuccess(function(evnet, jqXHR, jqSettingss) {
    //     if (jqSettingss.url == ajaxRequestUrl) {
    //         console.log("ajax requeset success, url is " + jqSettingss.url + ",result:" + event.result + " response:" + jqXHR.response + "," + jqSettingss.result);
    //     }
    // })

    // $.ajax({
    //     url: ajaxRequestUrl,
    //     data: {
    //         zipcode: 97201
    //     },
    //     success: function(result) {
    //         console.log("result:", result);
    //     }
    // });

    // $.ajax({
    //     url: "",
    //     data: {
    //         zipcode: 97201
    //     },
    //     success: function(result) {
    //         //alert("123"); 
    //         // console.log("result:{0}",result);
    //         // $("#weather-temp").html("<strong>" + result + "</strong> degrees");
    //     }
    // });
})

/****************************index.cshtml end******************************/