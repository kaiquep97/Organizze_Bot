using Microsoft.Bot.Builder.FormFlow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizzeBot.Models
{
    [Serializable]
    public class Movimentacao
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("description")]
        [Prompt("Qual a descrição da Movimentação?")]
        public string Description { get; set; }
        [JsonProperty("date")]
        [Prompt("Qual a data?")]
        public string Date { get; set; }
        [JsonProperty("paid")]
        public bool Paid { get; set; }
        [JsonProperty("amount_cents")]
        public int Amount_cents { get; set; }
        [JsonProperty("total_installments")]
        public int Total_installments { get; set; }
        [JsonProperty("installment")]
        public int Installment { get; set; }
        [JsonProperty("recurring")]
        public bool Recurring { get; set; }
        [JsonProperty("account_id")]
        public int Account_id { get; set; }
        [JsonProperty("category_id")]
        public int Category_id { get; set; }
        [JsonProperty("contact_id")]
        public object Contact_id { get; set; }
        [JsonProperty("notes")]
        public string Notes { get; set; }
        [JsonProperty("attachments_count")]
        public int Attachments_count { get; set; }
        [JsonProperty("credit_card_id")]
        public object Credit_card_id { get; set; }
        [JsonProperty("credit_card_invoice_id")]
        public object Credit_card_invoice_id { get; set; }
        [JsonProperty("paid_credit_card_id")]
        public object Paid_credit_card_id { get; set; }
        [JsonProperty("paid_credit_card_invoice_id")]
        public object Paid_credit_card_invoice_id { get; set; }
        [JsonProperty("oposite_transaction_id")]
        public object Oposite_transaction_id { get; set; }
        [JsonProperty("oposite_account_id")]
        public object Oposite_account_id { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime Updated_at { get; set; }
    }
}