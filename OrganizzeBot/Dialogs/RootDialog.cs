using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace OrganizzeBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {


        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (message.Value != null)
            {
                dynamic value = message.Value;
                string submitType = value.Type.ToString();
                switch (submitType)
                {
                    case "SaveCommand":
                        var valor = Convert.ToInt32(value.Valor.ToString().Replace(".","")) ;
                        var mov = Models.Movimentacao.Parse(value);

                        using(var client = new Services.MovimentacaoService())
                        {
                            await client.AddMovimentacao(mov);
                        }

                        await context.PostAsync("Adicionado com sucesso!");

                        break;
                    default:
                        break;
                }
            }
            else
            {
                ShowOptions(context);
            }
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string> { "Consultar movimentação","Adicionar Movimentação" },"**Olá eu sou o OrganizzeBot,**\nO que você deseja?","Não entendi");
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case "Consultar movimentação":
                        await MontaAdaptiveCards(context);
                        break;
                    case "Adicionar Movimentação":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task MontaAdaptiveCards(IDialogContext context)
        {
            try
            {
                //var txt = File.ReadAllText($@"C:\Users\User\source\repos\Organizze_Bot\OrganizzeBot\AdaptiveCards\card.json");
                //AdaptiveCardParseResult result = AdaptiveCard.FromJson(txt);

                //AdaptiveCard card = result.Card;

                Models.Categoria[] categorias;
                var choices = new List<AdaptiveChoice>();

                using (var client = new Services.CategoriaService())
                {
                    categorias = await client.BuscaCategorias();

                    if(categorias != null)
                    {
                        foreach (var categoria in categorias)
                        {
                            var choice = new AdaptiveChoice()
                            {
                                Title = categoria.Name,
                                Value = categoria.Id.ToString()
                            };

                            choices.Add(choice);
                        }
                    }
                }


                var card = new AdaptiveCard()
                {
                    Body = new List<AdaptiveElement>()
                    {
                        new AdaptiveTextBlock()
                        {
                            Text = "Adicionar Movimentação",
                            Size = AdaptiveTextSize.Medium,
                            Weight = AdaptiveTextWeight.Bolder,
                            HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                        },
                        new AdaptiveTextInput()
                        {
                            Placeholder = "Descrição",
                            Style = AdaptiveTextInputStyle.Text,
                            Id = "Descricao"
                        },
                        new AdaptiveNumberInput()
                        {
                            Placeholder = "Valor",
                            Min = 0,
                            Id = "Valor"
                        },
                        new AdaptiveDateInput()
                        {
                            Placeholder = "Data",
                            Value = DateTime.Now.ToString("yyyy-MM-dd"),
                            Id = "Data"
                        },
                        new AdaptiveChoiceSetInput()
                        {
                            Style = AdaptiveChoiceInputStyle.Compact,
                            Id = "Categoria",
                            Choices = choices
                        }
                    },
                    Actions = new List<AdaptiveAction>()
                    {
                        new AdaptiveSubmitAction()
                        {
                            Title = "Salvar",
                            DataJson = "{ \"Type\": \"SaveCommand\" }"
                        }
                    }
                };





                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                var reply = context.MakeMessage();
                reply.Attachments.Add(attachment);

                await context.PostAsync(reply, CancellationToken.None);
                context.Wait(MessageReceivedAsync);

            }
            catch (AdaptiveSerializationException ex)
            {

                throw ex;
            }
        }
    }
}