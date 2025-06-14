using InfrastructureToolKit.Interfaces.QrCodes.QrCoder;
using InfrastructureToolKit.QrCodes.QrCoder.Settings;
using QRCoder;
using System.Drawing;

namespace InfrastructureToolKit.QrCodes.QrCoder.QrCoder
{
    public class QrCoder : IQrCoder
    {
        private QrCoderSettings qrCoderSettings;

        public QrCoder(QrCoderSettings settings)
        {
            qrCoderSettings = settings;
        }

        //public virtual async Task GenerateQrCodeAsync(QrCoderGenerateSettings settings)
        //{
        //    QRCodeGenerator generator = new QRCodeGenerator();
        //    QRCodeData data = null;

        //    if (qrCoderSettings.Payload != null && qrCoderSettings.ECCLevel == null)
        //        data = generator.CreateQrCode(await SetPayload());
        //    else if (qrCoderSettings.Payload == null && qrCoderSettings.ECCLevel != null)
        //        data = generator.CreateQrCode(settings.BinaryData, await SetECCLevel());
        //    else if (qrCoderSettings.Payload != null && qrCoderSettings.ECCLevel != null)
        //        data = generator.CreateQrCode(await SetPayload(), await SetECCLevel());
        //    else
        //        data = generator.CreateQrCode(settings.Text, await SetECCLevel(), qrCoderSettings.ForceUtf8, qrCoderSettings.Utf8BOM, await SetEciMode(), qrCoderSettings.RequestedVersion);

        //    var qrCode = new QRCoder.QRCode(data); // não use apenas "QRCode"

        //    var logo = new Bitmap(settings.PathLogo);
        //    var imagemFinal = qrCode.GetGraphic(
        //        data: data,
        //        pixelsPerModule: qrCoderSettings.PixelsPerModule,
        //        darkColor: qrCoderSettings.ColorBackground,
        //        lightColor: qrCoderSettings.ColorFront,
        //        icon: logo,
        //        iconSizePercent: qrCoderSettings.IconSizePercent,
        //        iconBorderWidth: qrCoderSettings.IconBorderWidth,
        //        drawQuietZones: qrCoderSettings.DrawQuietZones
        //    );

        //    imagemFinal.Save(settings.OutPut, await SetOutputFormat());
        //}

        //private async Task<PayloadGenerator.Payload> SetPayload()
        //{
        //    PayloadGenerator.Payload payload = null;

        //    if (qrCoderSettings.Payload != null)
        //    {
        //        switch (qrCoderSettings.Payload)
        //        {
        //            case Payload.BezahlCode:
        //                payload = new PayloadGenerator.BezahlCode(/* parâmetros */);
        //                break;
        //            case Payload.BitcoinAddress:
        //                payload = new PayloadGenerator.BitcoinAddress("enderecoBitcoin");
        //                break;
        //            case Payload.BitcoinCashAddress:
        //                payload = new PayloadGenerator.BitcoinCashAddress("enderecoBCH");
        //                break;
        //            case Payload.BitcoinLikeCryptoCurrencyAddress:
        //                payload = new PayloadGenerator.BitcoinLikeCryptoCurrencyAddress("Moeda", "Endereco");
        //                break;
        //            case Payload.Bookmark:
        //                payload = new PayloadGenerator.Bookmark("Título", "https://exemplo.com");
        //                break;
        //            case Payload.CalendarEvent:
        //                payload = new PayloadGenerator.CalendarEvent("Evento", "Local", "Descrição", DateTime.Now, DateTime.Now.AddHours(1));
        //                break;
        //            case Payload.ContactData:
        //                payload = new PayloadGenerator.ContactData(PayloadGenerator.ContactData.ContactOutputType.VCard3, "Nome");
        //                break;
        //            case Payload.Geolocation:
        //                payload = new PayloadGenerator.Geolocation(51.3, 6.6);
        //                break;
        //            case Payload.Girocode:
        //                payload = new PayloadGenerator.Girocode("Nome", "IBAN", "EUR", "Descrição");
        //                break;
        //            case Payload.LitecoinAddress:
        //                payload = new PayloadGenerator.LitecoinAddress("enderecoLitecoin");
        //                break;
        //            case Payload.Mail:
        //                payload = new PayloadGenerator.Mail("email@exemplo.com", "Assunto", "Mensagem");
        //                break;
        //            case Payload.MMS:
        //                payload = new PayloadGenerator.MMS("+5521999999999", "Mensagem MMS");
        //                break;
        //            case Payload.MoneroTransaction:
        //                payload = new PayloadGenerator.MoneroTransaction("endereco", "123", "memo opcional");
        //                break;
        //            case Payload.OneTimePassword:
        //                payload = new PayloadGenerator.OneTimePassword("label", "secret", PayloadGenerator.OneTimePassword.AuthenticatorType.TOTP, 6);
        //                break;
        //            case Payload.PhoneNumber:
        //                payload = new PayloadGenerator.PhoneNumber("+5521999999999");
        //                break;
        //            case Payload.RussiaPaymentOrder:
        //                payload = new PayloadGenerator.RussiaPaymentOrder(/* parâmetros */);
        //                break;
        //            case Payload.ShadowSocksConfig:
        //                payload = new PayloadGenerator.ShadowSocksConfig("host", 443, "password", "aes-256-cfb");
        //                break;
        //            case Payload.SkypeCall:
        //                payload = new PayloadGenerator.SkypeCall("usuarioSkype");
        //                break;
        //            case Payload.SlovenianUpnQr:
        //                payload = new PayloadGenerator.SlovenianUpnQr("IBAN", "Beneficiário", "Endereço", "Cidade", "Código", "Referência", "Descrição", "Importância");
        //                break;
        //            case Payload.SMS:
        //                payload = new PayloadGenerator.SMS("+5521999999999", "Mensagem SMS");
        //                break;
        //            case Payload.SwissQrCode:
        //                payload = new PayloadGenerator.SwissQrCode(/* parâmetros */);
        //                break;
        //            case Payload.Url:
        //                payload = new PayloadGenerator.Url("https://exemplo.com");
        //                break;
        //            case Payload.WhatsAppMessage:
        //                payload = new PayloadGenerator.WhatsAppMessage("+5521999999999", "Mensagem via WhatsApp");
        //                break;
        //        }
        //    }

        //    return await Task.FromResult(payload);
        //}

        //private async Task<QRCodeGenerator.ECCLevel> SetECCLevel()
        //{
        //    if (qrCoderSettings.ECCLevel != null)
        //    {
        //        return await Task.FromResult((QRCodeGenerator.ECCLevel)qrCoderSettings.ECCLevel);
        //    }
        //    return await Task.FromResult(QRCodeGenerator.ECCLevel.Default);
        //}

        //private async Task<QRCodeGenerator.EciMode> SetEciMode()
        //{
        //    if (qrCoderSettings.EciMode != null)
        //    {
        //        return await Task.FromResult((QRCodeGenerator.EciMode)qrCoderSettings.EciMode);
        //    }
        //    return await Task.FromResult(QRCodeGenerator.EciMode.Default);
        //}

        //private async Task<OutputFormat> SetOutputFormat()
        //{
        //    if (qrCoderSettings.OutputFormat != null)
        //    {
        //        return await Task.FromResult((OutputFormat)qrCoderSettings.OutputFormat);
        //    }
        //    return await Task.FromResult(OutputFormat.Png);
        //}

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
