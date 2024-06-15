﻿using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository;

public interface ITipoMovimentacaoRepository
{
    Task<TipoMovimentacaoEntity> CriarTipoMovimentacaoAsync(TipoMovimentacaoEntity tipoMovimentacao);
    Task<TipoMovimentacaoEntity> ObterTipoMovimentacaoAsync(int id);
    Task<List<TipoMovimentacaoEntity>> ObterTodosTiposMovimentacaoAsync();
    Task<TipoMovimentacaoEntity> AtualizarTipoMovimentacaoAsync(TipoMovimentacaoEntity tipoMovimentacao);
    Task<TipoMovimentacaoEntity> DeletarTipoMovimentacaoAsync(int id);
}