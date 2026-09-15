using System.Globalization;
using System.Net;
using DriverHub.Application.Contracts.Communication.Mail;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;

namespace DriverHub.Application.Common.EmailTemplates;

public static class ReservationEmailTemplate
{
    public static SendMailRequest Create(Reservation reservation, string vehicle, string pickupLocation)
    {
        string status = reservation.Status switch
        {
            ReservationStatus.Pending => "Talebiniz alındı; onay bekliyor (Pending).",
            ReservationStatus.Confirmed => "Rezervasyonunuz onaylandı (Approved).",
            ReservationStatus.Cancelled => "Rezervasyonunuz iptal edildi (Cancelled).",
            _ => throw new ArgumentOutOfRangeException(nameof(reservation.Status))
        };
        string subject = reservation.Status switch
        {
            ReservationStatus.Pending => "DriverHub — Rezervasyon talebiniz alındı",
            ReservationStatus.Confirmed => "DriverHub — Rezervasyonunuz onaylandı",
            _ => "DriverHub — Rezervasyonunuz iptal edildi"
        };
        string body = $"""
            <h1>Rezervasyon bilgileri</h1>
            <p>Merhaba {WebUtility.HtmlEncode(reservation.CustomerFirstName)} {WebUtility.HtmlEncode(reservation.CustomerLastName)},</p>
            <p>{status}</p>
            <dl>
              <dt>Rezervasyon</dt><dd>{reservation.Id}</dd>
              <dt>Araç</dt><dd>{WebUtility.HtmlEncode(vehicle)}</dd>
              <dt>Başlangıç (UTC)</dt><dd>{reservation.StartDate:yyyy-MM-dd HH:mm}</dd>
              <dt>Bitiş (UTC)</dt><dd>{reservation.EndDate:yyyy-MM-dd HH:mm}</dd>
              <dt>Teslim alma</dt><dd>{WebUtility.HtmlEncode(pickupLocation)}</dd>
              <dt>Toplam (TRY)</dt><dd>{reservation.TotalPrice.ToString("F2", CultureInfo.InvariantCulture)}</dd>
            </dl>
            """;
        return new SendMailRequest(reservation.CustomerEmail!, subject, body);
    }
}
