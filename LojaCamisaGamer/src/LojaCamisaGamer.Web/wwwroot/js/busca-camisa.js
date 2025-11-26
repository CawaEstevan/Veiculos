document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('searchInput');
    const camisasContainer = document.getElementById('camisasContainer');
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

                fetch(`/Camisa/Search?termo=${encodeURIComponent(termo)}`)
                    .then(response => response.json())
                    .then(data => {
                        renderCamisas(data);
                    })
                    .catch(error => {
                        console.error('Erro ao buscar camisas:', error);
                    });
            }, 300);
        });
    }

    function renderCamisas(camisas) {
        if (camisas.length === 0) {
            camisasContainer.innerHTML = `
                <div class="col-12">
                    <div class="alert alert-info">
                        <i class="bi bi-info-circle"></i> Nenhuma camisa encontrada.
                    </div>
                </div>
            `;
            return;
        }

        let html = '<div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">';
        
        camisas.forEach(camisa => {
            const statusClass = camisa.disponivel ? 'bg-success' : 'bg-danger';
            const statusText = camisa.disponivel ? 'Disponível' : 'Indisponível';
            const descricao = camisa.descricao.substring(0, Math.min(100, camisa.descricao.length)) + '...';
            const preco = new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(camisa.preco);
            
            const imagemHtml = camisa.imagemUrl 
                ? `<img src="${camisa.imagemUrl}" class="card-img-top" alt="${camisa.nome}" style="height: 200px; object-fit: cover;">`
                : `<div class="card-img-top bg-secondary d-flex align-items-center justify-content-center" style="height: 200px;">
                     <i class="bi bi-image text-white" style="font-size: 3rem;"></i>
                   </div>`;

            html += `
                <div class="col">
                    <div class="card h-100 shadow-sm">
                        ${imagemHtml}
                        <div class="card-body">
                            <h5 class="card-title">${camisa.nome}</h5>
                            <p class="text-muted small mb-2">
                                <i class="bi bi-tag"></i> ${camisa.categoriaNome || 'Sem categoria'}
                            </p>
                            <p class="card-text text-muted small">${descricao}</p>
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <h4 class="text-success mb-0">${preco}</h4>
                                <span class="badge bg-info">${camisa.tamanho}</span>
                            </div>
                            <div class="mb-2">
                                <small class="text-muted">
                                    <i class="bi bi-palette"></i> ${camisa.cor}
                                </small>
                            </div>
                            <div class="mb-2">
                                <span class="badge ${statusClass}">${statusText}</span>
                                <span class="badge bg-secondary">Estoque: ${camisa.estoque}</span>
                            </div>
                        </div>
                        <div class="card-footer bg-transparent">
                            <div class="btn-group w-100" role="group">
                                <a href="/Camisa/Details/${camisa.id}" class="btn btn-sm btn-info">
                                    <i class="bi bi-eye"></i>
                                </a>
                                <a href="/Camisa/Edit/${camisa.id}" class="btn btn-sm btn-warning">
                                    <i class="bi bi-pencil"></i>
                                </a>
                                <a href="/Camisa/Delete/${camisa.id}" class="btn btn-sm btn-danger">
                                    <i class="bi bi-trash"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });

        html += '</div>';
        camisasContainer.innerHTML = html;
    }
});