namespace ClinicaEscolaBase.Models;

// Exemplo de Value Object para o Endereço
public record Endereco(
    string Logradouro, 
    string Numero, 
    string Bairro, 
    string Cidade, 
    string Estado, 
    string CEP
);