// Keep ReservationTimePolicy.StartGracePeriodMinutes in the API in sync.
export const RESERVATION_START_GRACE_PERIOD_MINUTES = 5;

export function minimumReservationStart(now = new Date()): Date {
  return new Date(Math.floor(now.getTime() / 60_000) * 60_000
    - RESERVATION_START_GRACE_PERIOD_MINUTES * 60_000);
}
