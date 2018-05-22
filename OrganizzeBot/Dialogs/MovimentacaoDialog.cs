using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using OrganizzeBot.Models;

namespace OrganizzeBot.Dialogs
{
    [Serializable]
    public class MovimentacaoDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }


        private void Adiciona(IDialogContext context)
        {
            var formDialog = FormDialog.FromForm<Movimentacao>(this.BuildForm, FormOptions.PromptInStart);

            context.Call(formDialog,ResumeAfterMovimentacaoFormDialog);

        }

        private async Task ResumeAfterMovimentacaoFormDialog(IDialogContext context, IAwaitable<Movimentacao> result)
        {
            try
            {
                var movement = await result;
                await context.PostAsync($"{movement.Description}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IForm<Movimentacao> BuildForm()
        {
            return new FormBuilder<Movimentacao>().Build();
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as IMessageActivity;

            // TODO: Put logic for handling user message here

            context.Wait(MessageReceivedAsync);
        }
    }
}