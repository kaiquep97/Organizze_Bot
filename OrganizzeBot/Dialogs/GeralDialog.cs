using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using OrganizzeBot.Services;

namespace OrganizzeBot.Dialogs
{
    [Serializable]
    [LuisModel("1881aa00-94b5-41a9-b569-013af9837b1f", "79055c66e768418a85742c35a22c07ec")]
    public class GeralDialog : LuisDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            ShowOptions(context);

            return Task.CompletedTask;
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string> { "Consultar movimentação", "Adicionar Movimentação" }, "**Olá eu sou o OrganizzeBot,**\nO que você deseja?", "Não entendi");
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case "Consultar movimentação":
                        break;
                    case "Adicionar Movimentação":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Não consegui entender a frase");
        }
        [LuisIntent("Sobre")]
        public async Task Sobre(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Eu sou um bot");
        }
        [LuisIntent("Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("**Olá, eu sou um bot**");
        }

        [LuisIntent("Movimentacao")]
        public async Task Movimentacao(IDialogContext context,LuisResult result)
        {

            var entidades = result.Entities?.Select(e => e.Type);

            var mes = result.Entities?.Where(x => x.Type.Equals("mes")).Select(e => e.Entity);
            var categoria = result.Entities?.Where(x => x.Type.Equals("categoria")).Select(e => e.Entity);

            await context.PostAsync($"beibe do beibe {string.Join(",",entidades.ToArray())} ");

        }
    }
}