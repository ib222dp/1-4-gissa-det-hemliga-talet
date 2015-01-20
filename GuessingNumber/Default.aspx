<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GuessingNumber.Default"
    ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gissa det hemliga talet</title>
    <link href="~/Content/guess.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <h1>Gissa det hemliga talet</h1>
        </div>
        <div id="main">
            <%-- Felmeddelanden --%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Ett fel inträffade. Korrigera felet och gör ett nytt försök."
                CssClass="validation" />
            <%-- Gissning --%>
            <div id="guessnumber">
                <asp:Label ID="Label1" runat="server" Text="Ange ett tal mellan 1 och 100:" AssociatedControlID="GuessedNumber"></asp:Label>
                <asp:TextBox ID="GuessedNumber" runat="server" TextMode="Number"></asp:TextBox>
                <%-- Validering --%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ett tal måste anges."
                    ControlToValidate="GuessedNumber" SetFocusOnError="True" Display="Dynamic" Text="*"
                    CssClass="validation"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="GuessedNumber"
                    SetFocusOnError="True" Type="Integer" Display="Dynamic" ErrorMessage="Ange ett tal mellan 1 och 100."
                    Text="*" MaximumValue="100" MinimumValue="1" CssClass="validation"></asp:RangeValidator>
                <%-- Kommandoknapp --%>
                <asp:Button ID="GuessButton" runat="server" Text="Skicka gissning" OnClick="GuessButton_Click" />
            </div>
            <%-- Tidigare gissningar samt resultat av senaste gissningen --%>
            <div id="guesses">
                <asp:PlaceHolder ID="GuessPlaceHolder" runat="server" Visible="false">
                    <asp:ListBox ID="GuessListBox" runat="server"></asp:ListBox>
                    <asp:Label ID="GuessLabel" runat="server"></asp:Label>
                </asp:PlaceHolder>
            </div>
            <%-- Knapp för nytt hemligt tal --%>
            <div id="newnobutton">
                <asp:Button ID="NewNoButton" runat="server" Text="Skapa nytt hemligt tal" Visible="false"
                    OnClick="NewNoButton_Click" CausesValidation="false" />
            </div>
        </div>
    </form>
</body>
</html>
