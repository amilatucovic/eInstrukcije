using System.ComponentModel.DataAnnotations.Schema;
namespace RS1_2024_2025.Domain.Entities
{
    // ReservationPayments

    // Napuniti je kada se approva rezervacija sa statusom Paid=Pending
    // Kada se stvarno plati ova se tablica azurira na Paid
    // ReservationPaymentStatus dodati
    // Reason dodatai
    // Kada se cancel rezervacija da se ovdje postavi status na Cancelled 
    // Kada se cancel rezervacija napuniti i reason Due to tutor cancellation i azurirati status rezervavacije Cancelled

    public enum ReservationPaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

    public class ReservationPayment : IMyBaseEntity
    {
        public int ID { get; set; }

        [ForeignKey(nameof(Reservation))]
        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public ReservationPaymentStatus Status { get; set; }

        // Koliko tutoru dajemo od ovog placenog casa
        // Kada se punio ovaj zapis u c# odmah napisati 10% ide sistemu, od amounta odbit 10%,
        // ostatak assignat na TutorExpencesAmount
        public decimal TutorExpencesAmount { get; set; }
        public bool TutorExpencesPaid { get; set; }
    }

    // Ako uzmemo pretpostavku da tutora redovno isplacujemo, mozemo uvijek sabrati TutorExpencesAmount i
    //
    // znati koliko mu je isplaceno
}
// ReservationPayments

// Napuniti je kada se approva rezervacija sa statusom Paid=Pending
// Kada se stvarno plati ova se tablica azurira na Paid
// ReservationPaymentStatus dodati
// Reason dodatai
// Kada se cancel rezervacija da se ovdje postavi status na Cancelled 
// Kada se cancel rezervacija napuniti i reason Due to tutor cancellation i azurirati status rezervavacije Cancelled

// OPCIJE
// POMJERI REZERVACIJUJ/TERMIN
// PONISTI

// POMJERI REZERVACIJU SAMO VALIDACIJA DA LI NA TOM TERMINU NOVOM ZA TUG  TUTORA POSTOJI VEC TERMIN U BAZI
// WHERE TUTOR = AMILA AND RESERVATIONDATE = NEKIDATUM I STATUS == APPROVED


//ReservationId

//Amount 50KM

//Status Paid

//TutorExpences (30KM)

//TutorExpencesPaid / boolean

// user badges, stoji u bazi kako i jeste
// Otvoris tutora: BROJ ODRZANIH CASOVA 
// BROJ ODRZAVANIH CASOVA = SELECT ALL FROM RESERVAATIONPAYMENTS WHERE Status = PAID ZA TOG TUTORA
// NPR SELECT * FROM RESERVATIONPAYMENTS WHERE TUTORID=10 AND RESERVATIONSTATUS = PAID

// UserBadgesCriteria

// ID NumberOfPoints RelatedTo (Enum, student, tutor) Title
// 1  100   Tutor  Junior
// 2  200   Tutor  Medior
// 3  400   Tutor   Senior

//Kada student plati onda pozvati jos jednu metodu koja ce automatski azurirati tutora
//Tipa student plati, okine se metoda CheckTutorBadge()
//CheckTutorBadge ode na bazu prebroji sve tutorove odrzane casove, iz tablice reservatrion payments
//gdje je status paid, npr vrati mu taj upit 150 i onda napravi querz na tablicu UserBadgesCriteria da provjeri
//je li tutor upao u neki novi range.