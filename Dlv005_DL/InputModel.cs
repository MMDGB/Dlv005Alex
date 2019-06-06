using System;

namespace Dlv005_DL
{
    public class InputModel
    {
        public string DL31_KOMM_ANFORDERUNG_NR { get; set; }
        public string DL31_ERPROBUNGSINHALT { get; set; }
        public DateTime DL31_START_DATUM { get; set; }
        public DateTime DL31_ENDE_DATUM { get; set; }
        public string DL38_BEZEICHNUNG { get; set; }
        public string DL39_BEZEICHNUNG { get; set; }
        public string DL40_BEZEICHNUNG { get; set; }
        public string DL31_BAUREIHEN { get; set; }
        public decimal? DL31_AUFTRAGGEBER_PERSID { get; set; }
        public string DL37_BEZEICHNUNG { get; set; }
        public decimal? DL31_KOMM_ERPROBUNGSORT_ID { get; set; }
        public decimal? DL31_KOMM_STRECKENART_ID { get; set; }
        public decimal? DL31_KOMM_ERPROBUNGSART_ID { get; set; }
        public decimal? DL31_AUFTRAGGEBER_OE { get; set; }
        public decimal? DL31_FAHRTENLEITER_PERSID { get; set; }
        public decimal? DL31_ENGINEERING_AST_PERSID { get; set; }
        public decimal? DL31_HV_QUALIFIKATION_ID { get; set; }
        public decimal? DL31_SONDERQUALIFIKATION_ID { get; set; }
        public decimal? DL31_PERSID_AENDERNG { get; set; }
        public decimal? DL31_PERSID_ERFASSNG { get; set; }
        public DateTime DL31_DATUM_AENDERNG { get; set; }
        public DateTime DL31_DATUM_ERFASSNG { get; set; }
        public string BD255_BEZEICHNUNG { get; set; }
        public decimal? DL31_FAHRBERECHTIGUNG_ID { get; set; }
        public decimal? DL31_KOMM__STATUS_ID { get; set; }
        public string DL31_SAMSTAGSARBEIT { get; set; }
        public string DL31_SONNTAGSARBEIT { get; set; }
        public decimal? DL31_KOMM_ANFORDERUNG_ID { get; set; }
    }

    public class AllocationInputModel
    {
        public string DL32_KONTIERUNG { get; set; }
        public decimal? DL32_ANTEIL_PROZENT { get; set; }
        public decimal? DL32_EXT_KOMM_ANFORDERUNG_ID { get; set; }
        public decimal? DL32_KOMM_ANFORDERUNG_KONTO_ID { get; set; }
    }
}