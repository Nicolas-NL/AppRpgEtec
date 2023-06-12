﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRpgEtec.Service.Personagens;
using AppRpgEtec.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Personagens
{
    public class ListagemPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;

        public ObservableCollection<Personagem> Personagens { get; set; }

        public ListagemPersonagemViewModel()
        {

            string token = Preferences.Get("UsuarioToken ", string.Empty);
            pService = new PersonagemService(token);
            Personagens = new ObservableCollection<Personagem>();

            _ = ObterPersonagens();

            NovoPersonagem = new Command(async () => { await ExibirCadastroPersonagem(); });
            RemoverPersonagemCommand =
                new Command<Personagem>(async (Personagem p) => { await RemoverPersonagem(p); });
        }
        public ICommand NovoPersonagem { get; }

        public ICommand RemoverPersonagemCommand { get; }

        public async Task ObterPersonagens()
        {
            try //Junto com o Cacth evitará que erros fechem o aplicativo
            {
                Personagens = await pService.GetPersonagensAsync();
                OnPropertyChanged(nameof(Personagens)); //Informará a View que houve carregamento
            }
            catch (Exception ex)
            {
                //Captará o erro para exibir em tela
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes" + ex.InnerException, "Ok");
            }
        }
        public async Task ExibirCadastroPersonagem()
        {
            try
            {
                await Shell.Current.GoToAsync("cadPersonagemView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.
                    DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public async Task RemoverPersonagem(Personagem p)
        {
            try
            {
                if (await Application.Current.MainPage
                    .DisplayAlert("Confirmação", $"Confirma a remoção de {p.Nome}?", "sim", "Não"))
                {
                    await pService.DeletePersonagemAsync(p.Id);
                    await Application.Current.MainPage.DisplayAlert("Mensagem",
                        "Personagem removido com sucesso!", "Ok");

                    _ = ObterPersonagens();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
