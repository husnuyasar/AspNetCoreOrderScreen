#pragma checksum "c:\Users\Vito\Documents\Asp.Net Core\OrderScreen\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "635d06ed004616b48e1e6e452ba9dcf1fea26c59"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "c:\Users\Vito\Documents\Asp.Net Core\OrderScreen\Views\_ViewImports.cshtml"
using OrderScreen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "c:\Users\Vito\Documents\Asp.Net Core\OrderScreen\Views\_ViewImports.cshtml"
using OrderScreen.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"635d06ed004616b48e1e6e452ba9dcf1fea26c59", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"219f67dd5bd3a4dce338a773ad1dd9f9b5e3da75", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "c:\Users\Vito\Documents\Asp.Net Core\OrderScreen\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js""></script>
<script type=""text/javascript"">
    var productList;
    $( document ).ready(function() {
        
        $.ajax({
            type: 'POST',
            url : '/Home/GetProducts',
            dataType: 'json',
            success: function(result){
                productList = result;
                $.each(result, function(key, value) {   
                    $('#productName')
                        .append($(""<option></option>"")
                                    .attr(""value"",value.id)
                                    .text(value.product_name)); 
                });
                $(""#price"").text(result[0].price);
            }
        });

        $.ajax({
            type: 'POST',
            url : '/Home/GetWareHouses',
            dataType: 'json',
            success: function(result){
                $.each(result, function(key, value) {   
                    $('#warehouseName");
            WriteLiteral(@"')
                        .append($(""<option></option>"")
                                    .attr(""value"",value.id)
                                    .text(value.warehouse_name)); 
                });
            }
        });

        $( ""#productName"" ) .change(function () {  
            var selectedID = $( ""#productName option:selected"" ).val();
            $.each(productList, function(key, value){
                if(value.id == selectedID){
                    $(""#price"").text(value.price);
                    $(""#count"").val("""");
                    $(""#discount"").val("""");
                }
            });
        }); 
        $(""#count"").change(function() { 
            var count = $(""#count"").val();
            if(count>0){
                var selectedID = $( ""#productName option:selected"" ).val();
                var price;
                $.each(productList, function(key, value){
                    if(value.id == selectedID){
                        price = value.price;");
            WriteLiteral(@"
                    }
                });
                $(""#price"").text(count*price);
            }
            else{
                alert(""Lüften doğru adet seçimi yapınız."");
                var selectedID = $( ""#productName option:selected"" ).val();
                $.each(productList, function(key, value){
                    if(value.id == selectedID){
                        $(""#price"").text(value.price).toFixed(2);
                    }
                });
            }
        }); 
        $(""#discount"").change(function() { 
            var discount = $(""#discount"").val();
            if(discount>0){
                var selectedID = $( ""#productName option:selected"" ).val();
                var price;
                $.each(productList, function(key, value){
                    if(value.id == selectedID){
                        price = value.price;
                    }
                });
                price = price - (price*discount/100);
                price = pars");
            WriteLiteral(@"eFloat(price).toFixed(2);
                $(""#price"").text(price);
            }
            else{
                alert(""Lüften doğru indirim miktarı seçimi yapınız."");
                var selectedID = $( ""#productName option:selected"" ).val();
                $.each(productList, function(key, value){
                    if(value.id == selectedID){
                        $(""#price"").text(value.price);
                    }
                });
            }
        });
    });
    
    function getCustomer()
    {
        var customerNo = $(""#customerno"").val();

        $.ajax({
            type: 'POST',
            url: '/Home/GetCustomer',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: customerNo,
            success: function (result) {
                if(result==-1)
                {
                    alert(""Müşteri numarısı 0, boş ya da harflerden oluşmaz."");
                    $(""#customername"").val("""");
    ");
            WriteLiteral(@"                $(""#customersurname"").val("""");
                }
                if(result == 0)
                {
                    alert(""Müşteri bulunamadı."");
                    $(""#customername"").val("""");
                    $(""#customersurname"").val("""");
                }
                else
                {
                    $(""#customername"").val(result.customer_name);
                    $(""#customersurname"").val(result.customer_surname);
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function AddOrderItem()
    {
        var productID = $( ""#productName option:selected"" ).val();
        var productName = $( ""#productName option:selected"" ).text();
        var count = $( ""#count"" ).val();
        if(count == """" || count == 0)
        {
            count ");
            WriteLiteral(@"= 1;
        }
        var discount = $( ""#discount"" ).val();   
        var warehouseID = $( ""#warehouseName option:selected"" ).val();
        var warehouseName = $( ""#warehouseName option:selected"" ).text();
        var price = $(""#price"").text();

        var dataObject = {
            'product_id': productID,
            'product_name': productName,
            'count': count,
            'discount': discount,
            'warehouse_id': warehouseID,
            'warehouse_name': warehouseName,
            'order_price': price
        }
        debugger;
        $.ajax({
            type : 'POST',
            url: '/Home/AddProductToSession',
            dataType: 'json',
            data: dataObject,
            success: function (result)
            {
                if(result!=null)
                {
                    var markup = ""<tr><td><input type='hidden' value=''""+result.product_id+""''><label>""+result.product_name+""</label></td>"";
                    markup+=""<td><lab");
            WriteLiteral(@"el>""+result.count+""</label></td>"";
                    markup+=""<td><label>""+result.discount+""</label></td>"";
                    markup+= ""<td><input type='hidden' value=''""+result.warehouse_id+""''><label>""+result.warehouse_name+""</label></td>"";
                    markup+=""<td><label>""+result.order_price+""</label> TL</td>"";
                    tableBody = $(""#orderItemTable""); 
                    tableBody.append(markup); 
                }
                console.log(result);
                
            }
        });
        
    }

    function Save()
    {
        var customerno = $(""#customerno"").val();
        var customername = $(""#customername"").val();
        var customersurname = $(""#customersurname"").val();
        var orderno = $(""#orderno"").val();
        var orderdate = $(""#orderdate"").val();

        var dataObject = {
            'customer_no': customerno,
            'customer_name': customername,
            'customer_surname': customersurname,
            'order");
            WriteLiteral(@"_no': orderno,
            'order_date': orderdate
        };

        $.ajax({
            type : 'POST',
            url: '/Home/Save',
            dataType: 'json',
            data: dataObject,
            success: function (result)
            {
                if(result == -1){
                    alert(""Eksik Bilgi."");
                }
                if(result == -2){
                    alert(""Sipariş kalemleri bulunamadı."");
                }
                if(result == -3){
                    alert(""Müşteri numarası ile müşteri uyuşmuyor."");
                }
                if(result == 1)
                {
                    alert(""Şipariş başarıyla eklenmiştir."");
                    window.location.href = '/Home/Index/'
                }
            }
        });
    }
</script>
<div class=""text-center"">
    <div class=""card"">
        <div class=""card-header"" style=""text-align:left"">
            Müşteri ve Siparişi Bilgileri
        </div>
        <div clas");
            WriteLiteral(@"s=""card-body"">
            <div class=""row"">
                <div class=""col-md-6"">
                    <div class=""form-row"">
                        <div class=""col-md-4 mb-3"">
                            <label>Müşteri Numarası: </label>
                        </div>
                        <div class=""col-md-4 mb-3"">
                            <input type=""text"" class=""form-control"" name=""customerno"" id=""customerno"">
                        </div>
                        <div class=""col-md-4 mb-3"">
                            <button class=""btn btn-primary"" onclick=""getCustomer()"">Ara</button>
                        </div>
                    </div>
                    <div class=""form-row"">
                        <div class=""col-md-4 mb-3"">
                            <label>Müşteri Adı: </label>
                        </div>
                        <div class=""col-md-4 mb-3"">
                            <input type=""text"" class=""form-control"" name=""customername"" id=""customername""");
            WriteLiteral(@">
                        </div>
                    </div>
                    <div class=""form-row"">
                        <div class=""col-md-4 mb-3"">
                            <label>Müşteri Soyadı: </label>
                        </div>
                        <div class=""col-md-4 mb-3"">
                            <input type=""text"" class=""form-control"" name=""customersurname"" id=""customersurname"">
                        </div>
                    </div>
                </div> 
                <div class=""col-md-6"">
                    <div class=""form-row"">
                        <div class=""col-md-4 mb-3"">
                            <label>Sipariş No: </label>
                        </div>
                        <div class=""col-md-4 mb-3"">
                            <input type=""text"" class=""form-control"" name=""orderno"" id =""orderno"">
                        </div>
                    </div>
                    <div class=""form-row"">
                        <div class=""c");
            WriteLiteral(@"ol-md-4 mb-3"">
                            <label>Sipariş Tarihi: </label>
                        </div>
                        <div class=""col-md-4 mb-3"">
                            <input type=""date"" class=""form-control"" name=""orderdate"" id=""orderdate"">
                        </div>
                    </div>
                </div> 

            </div>
        </div>
    </div>
    <div class=""row"" style=""float:right; margin-top:10px; margin-right:0px;"">
        <button class=""btn btn-primary"" onclick=""AddOrderItem()"">Sipariş Kalemi Ekle</button>
    </div>
    <div class=""card"" style=""margin-top:100px"">
        <div class=""card-header"" style=""text-align:left"">
            Sipariş Kalemleri
        </div>
        <div class=""card-body"">
            <table class=""table table-striped"" id=""orderItemTable"">
                <thead>
                    <tr>
                        <th>Ürün</th>
                        <th>Adet</th>
                        <th>İndirim Tutarı</th>
    ");
            WriteLiteral(@"                    <th>Depo</th>
                        <th>Toplam Fiyat</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <select class=""form-control form-control-sm"" id=""productName"" name=""productName"">
                            </select>
                        </td>
                        <td>
                            <input type=""text"" class=""form-control"" id=""count"">
                        </td>
                        <td>
                            <input type=""text"" class=""form-control"" id=""discount"">
                        </td>
                        <td>
                            <select class=""form-control form-control-sm"" id=""warehouseName"">
                            </select>
                        </td>
                        <td>
                            <label id=""price""></label> TL
                        </td>
                    </tr>");
            WriteLiteral(@"
                </tbody>
            </table>
        </div>
    
    
    </div>
    <div class=""row"" style=""float:right; margin-top:10px; margin-right:0px;"">
        <button class=""btn btn-primary"" onclick=""Save()"">Kaydet</button>
    </div>
</div>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
