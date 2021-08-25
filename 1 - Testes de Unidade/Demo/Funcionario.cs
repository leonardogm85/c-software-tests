using System;
using System.Collections.Generic;

namespace Demo
{
    public abstract class Pessoa
    {
        public string Nome { get; protected set; }
        public string Apelido { get; protected set; }
    }

    public class Funcionario : Pessoa
    {
        public double Salario { get; private set; }
        public NivelProfissional NivelProfissional { get; private set; }
        public IEnumerable<string> Habilidades { get; private set; }

        public Funcionario(string nome, double salario)
        {
            Nome = string.IsNullOrEmpty(nome)
                ? "Fulano"
                : nome;

            DefinirSalario(salario);
            DefinirHabilidades();
        }

        public void DefinirSalario(double salario)
        {
            if (salario < 500)
            {
                throw new Exception("Salario inferior ao permitido");
            }

            Salario = salario;

            if (salario < 2000)
            {
                NivelProfissional = NivelProfissional.Junior;
            }
            else if (salario < 8000)
            {
                NivelProfissional = NivelProfissional.Pleno;
            }
            else
            {
                NivelProfissional = NivelProfissional.Senior;
            }
        }

        public void DefinirHabilidades()
        {
            var habilidadesBasicas = new List<string>
            {
                "Lógica de Programação",
                "OOP"
            };

            if (NivelProfissional == NivelProfissional.Pleno || NivelProfissional == NivelProfissional.Senior)
            {
                habilidadesBasicas.Add("Testes");

                if (NivelProfissional == NivelProfissional.Senior)
                {
                    habilidadesBasicas.Add("Microservices");
                }
            }

            Habilidades = habilidadesBasicas;
        }
    }

    public enum NivelProfissional
    {
        Junior,
        Pleno,
        Senior
    }

    public class FuncionarioFactory
    {
        public static Funcionario Criar(string nome, double salario) => new Funcionario(nome, salario);
    }
}
