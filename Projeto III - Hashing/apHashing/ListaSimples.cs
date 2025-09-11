using System;
using System.Windows.Forms;

public class ListaSimples<Dado> where Dado : IComparable<Dado>
{
    NoLista<Dado> primeiro, ultimo, atual, anterior;

    int quantosNos;

    public NoLista<Dado> Primeiro => primeiro;
    public NoLista<Dado> Ultimo => ultimo;

    public ListaSimples()
    {
        primeiro = ultimo = null;
        quantosNos = 0;
    }

    public bool EstaVazia
    {
        get => primeiro == null;

        // get 
        // {
        //    if (primeiro == null)
        //       return true;
        //    return false;
        // }
    }
    public void InserirAposFim(Dado novoDado)
    {
        NoLista<Dado> novoNo = new NoLista<Dado>(novoDado);

        if (EstaVazia)
            primeiro = novoNo;
        else
            ultimo.Prox = novoNo;


        ultimo = novoNo;
        quantosNos++;
    }

    public void InserirAntesDoInicio(Dado novoDado)
    {
        var novoNo = new NoLista<Dado>(novoDado);

        if (EstaVazia)          // se a lista está vazia, estamos
            ultimo = novoNo;    // incluindo o 1o e o último nós!
        else
            novoNo.Prox = primeiro;

        primeiro = novoNo;
        quantosNos++;
    }

    public void ExibirNaTela()
    {
        atual = primeiro;
        while (atual != null)
        {
            Console.WriteLine(atual.Info);
            atual = atual.Prox;
        }
    }

    public void Listar(ListBox oListBox)
    {
        oListBox.Items.Clear(); // limpa o conteúdo do listBox
        atual = primeiro;
        while (atual != null)
        {
            oListBox.Items.Add(atual.Info);
            atual = atual.Prox;
        }
    }



    // Novo método para buscar um dado e retornar
    public Dado Buscar(Dado dadoProcurado)
    {
        atual = primeiro;
        while (atual != null)
        {
            if (atual.Info.CompareTo(dadoProcurado) == 0)
                return atual.Info;
            atual = atual.Prox;
        }
        return default(Dado);
    }

    // Novo método para remover um dado
    public bool Remover(Dado dadoARemover)
    {
        atual = primeiro;
        anterior = null;
        while (atual != null)
        {
            if (atual.Info.CompareTo(dadoARemover) == 0)
            {
                if (anterior == null) // Removendo o primeiro nó
                    primeiro = atual.Prox;
                else
                    anterior.Prox = atual.Prox;

                if (atual.Prox == null) // Atualizando 'ultimo' se necessário
                    ultimo = anterior;

                quantosNos--;
                return true;
            }
            anterior = atual;
            atual = atual.Prox;
        }
        return false;
    }

}