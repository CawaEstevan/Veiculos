document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('searchInput');
    const categoriasContainer = document.getElementById('categoriasContainer');
    let timeoutId;

    if (searchInput) {
        searchInput.addEventListener('input', function() {
            clearTimeout(timeoutId);
            
            const termo = this.value.trim();
            
            // Debounce: aguarda 300ms após o usuário parar de digitar
            timeoutId = setTimeout(function() {
                if (termo.length === 0) {
                    window.location.reload();
                    return;
                }

                if (termo.length < 2) {
                    return;
                }

                fetch(`/Categoria/Search?termo=${encodeURIComponent(termo)}`)
                    .then(response => response.json())
                    .then(data => {
                        renderCategorias(data);
                    })
                    .catch(error => {
                        console.error('Erro ao buscar categorias:', error);
                    });
            }, 300);
        });
    }

    function renderCategorias(categorias) {
        if (categorias.length === 0) {
            categoriasContainer.innerHTML = `
                <div class="col-12">
                    <div class="alert alert-info">
                        <i class="bi bi-info-circle"></i> Nenhuma categoria encontrada.
                    </div>
                </div>
            `;
            return;
        }

        let html = '<div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">';
        
        categorias.forEach(categoria => {
            const statusClass = categoria.ativa ? 'bg-success' : 'bg-secondary';
            const statusText = categoria.ativa ? 'Ativa' : 'Inativa';
            const dataFormatada = new Date(categoria.dataCriacao).toLocaleDateString('pt-BR');

            html += `
                <div class="col">
                    <div class="card h-100 shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title">${categoria.nome}</h5>
                            <p class="card-text text-muted">${categoria.descricao}</p>
                            <div class="mb-2">
                                <span class="badge ${statusClass}">${statusText}</span>
                            </div>
                            <small class="text-muted">
                                Cadastrada em: ${dataFormatada}
                            </small>
                        </div>
                        <div class="card-footer bg-transparent">
                            <div class="btn-group w-100" role="group">
                                <a href="/Categoria/Details/${categoria.id}" class="btn btn-sm btn-info">
                                    <i class="bi bi-eye"></i> Detalhes
                                </a>
                                <a href="/Categoria/Edit/${categoria.id}" class="btn btn-sm btn-warning">
                                    <i class="bi bi-pencil"></i> Editar
                                </a>
                                <a href="/Categoria/Delete/${categoria.id}" class="btn btn-sm btn-danger">
                                    <i class="bi bi-trash"></i> Excluir
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });

        html += '</div>';
        categoriasContainer.innerHTML = html;
    }
});
