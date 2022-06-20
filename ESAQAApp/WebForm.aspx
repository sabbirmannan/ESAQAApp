<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="BAC007.WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 8px;
        }
        .auto-style3 {
            width: 8px;
            text-align: center;
        }
        .auto-style4 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Gen" OnClick="Button1_Click" />
        </div>
    </form>

    rrrr
    <p>
        &nbsp;
        <table class="auto-style1">
            <tr>
                <td rowspan="3">নং</td>
                <td>১</td>
                <td colspan="7">২-৮</td>
                <td rowspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td rowspan="2">জমির প্রকার</td>
                <td rowspan="2">সেচ দেই?</td>
                <td colspan="6">বিভিন্ন পদ্ধতিতে সেচ</td>
            </tr>
            <tr>
                <td>গভীর নলকূপ</td>
                <td>অগভীর নলকূপ</td>
                <td>পাওয়ার পাম্প</td>
                <td>নলকূপ</td>
                <td>সেচনালা</td>
                <td>দেশীয় পদ্ধতি</td>
            </tr>
        </table>
    </p>





    <table class="auto-style1">
        <tr>
            <th class="text-center v-middle" rowspan="5">নং</th>
            <th class="text-center v-middle" rowspan="5">জমি</th>
            <th>১</th>
            <th class="text-center v-middle" colspan="5">২-৬</th>
            <th class="text-center v-middle" colspan="3">৭-৯</th>
            <th>১০</th>
            <th class="text-center v-middle" colspan="4">১১-১৪</th>
            <th class="text-center v-middle" rowspan="5">Action</th>
        </tr>
        <tr>
            <th class="text-center v-middle" rowspan="4">ফসলের কোড</th>
            <th class="text-center v-middle" colspan="5">সার ও কীটনাশক বাবদ খরচ</th>
            <th class="text-center v-middle" colspan="3">সেচ ও বীজ/চারা খরচ</th>
            <th class="text-center v-middle" rowspan="4">লাঙ্গল/ পাওয়ার টিলার (ভাড়া ও নিজস্ব) বাবদ ব্যয় (টাকা)</th>
            <th class="text-center v-middle" colspan="4">মজুর</th>
        </tr>
        <tr>
            <th class="text-center v-middle" colspan="2" rowspan="2">ইউরিয়া</th>
            <th class="text-center v-middle" colspan="2" rowspan="2">পটাশ/টিএসপি/অন্যান্য*</th>
            <th class="text-center v-middle" rowspan="3">কীটনাশক ইত্যাদি</th>
            <th class="text-center v-middle" rowspan="3">সেচের মোট ব্যয় (টাকা) (নিজস্ব ও ক্রয়কৃত)</th>
            <th class="text-center v-middle" colspan="2">বীজ/চারা**</th>
            <th class="text-center v-middle" rowspan="2">স্বনিয়োজিত শ্রম***</th>
            <th class="text-center v-middle" rowspan="2">ভাড়া করা মজুর</th>
            <th class="text-center v-middle" rowspan="2">দৈনিক গড় মজুরির হার****</th>
            <th class="text-center v-middle" rowspan="3">মোট ব্যয়</th>
        </tr>
        <tr>
            <th class="text-center v-middle">পরিমাণ</th>
            <th class="text-center v-middle">মূল্য</th>
        </tr>
        <tr>
            <th class="text-center v-middle">পরিমাণ (কেজি</th>
            <th class="text-center v-middle">মূল্য (টাকা)</th>
            <th class="text-center v-middle">পরিমাণ (কেজি</th>
            <th class="text-center v-middle">মূল্য (টাকা)</th>
            <th class="text-center v-middle">(কেজি)</th>
            <th class="text-center v-middle">(টাকা)</th>
            <th class="text-center v-middle">(শ্রম দিন)</th>
            <th class="text-center v-middle">(শ্রম দিন)</th>
            <th class="text-center v-middle">(টাকা)</th>
        </tr>
        <tr>
            <th colspan="2">contro 1</th>
            <th>contro 2</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
        </tr>
    </table>



    rrt

    
    <table class="auto-style1">
        <tr>
            <td rowspan="2">নং</td>
            <td>পরিবর্তনের ধরন</td>
            <td>অবস্থা</td>
            <td rowspan="2">উল্লেখিত পরিবর্তনে উপ-প্রকল্পের প্রভাব (কোড)</td>
            <td rowspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td>প্রকল্প এলাকার খানার জমি (চাষাধীন)</td>
            <td>বর্তমানে</td>
        </tr>
    </table>


    

    <table class="auto-style1">
        <tr>
            <td rowspan="2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td rowspan="2">&nbsp;</td>
            <td colspan="2">বিভিন্ন পদ্ধতিতে সেচ</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <table class="auto-style1">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    

    <table class="auto-style1">
        <tr>
            <td rowspan="2">নং</td>
            <td class="auto-style3" rowspan="2">ফসলের কোড</td>
            <td rowspan="2" class="auto-style4">ফসলের মোট উৎপাদিত(মণ)(সেকশন ২.৩ থেকে আসবে)</td>
            <td rowspan="2" class="auto-style4">উৎপাদিত পণ্যের মধ্যে নিজে ভোগ/ব্যবহারের জন্য (পরিমাণ-মণ)</td>
            <td rowspan="2" class="auto-style4">অবশিষ্ট/বাজারজাতকরণের জন্য (পরিমাণ-মণ)</td>
            <td colspan="2" class="auto-style4">মাঠে বিক্রিত ফসলের মূল্য (মণ প্রতি টাকায়)</td>
            <td colspan="4">বাজারে বিক্রিত পণ্যে মূল্য/মণ (বিগত বছরের গড়) (একাধিক বাজার হলে, বাজারের নাম, ফসলের নাম ও পরিমাণসহ উল্লেখ করুন)
            </td>
        </tr>
        <tr>
            <td>পরিমণ (মণ)</td>
            <td>প্রাপ্ত মূল্য (মণ প্রতি টাকায়)</td>
            <td>বাজারের নাম</td>
            <td>জমি থেকে বাজারের দূরত্ব (কিমি)</td>
            <td>যাতায়াত খরচ (মণ প্রতি)</td>
            <td>প্রাপ্ত মূল্য (মণ প্রতি টাকায়)</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    

</body>
</html>
