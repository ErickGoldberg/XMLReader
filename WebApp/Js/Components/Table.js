import { UseStore } from '../store/Main.js';
import Modal from './Modal.js';

export default {
    components: { Modal },
    template: `
    
    <div class="overlay">
      <modal :dataTableRef="this"/>
    </div> 
    <div class="table-container">
    <table class="table table-striped mt-5  compact" style="font-size: 0.78rem;" id="myTable">
      <thead >
        <tr>
          <th scope="col" style='width: 4%;'>Tipo de XML</th>
          <th scope="col" style='width: 6%;'>Número do XML</th>
          <th scope="col" style='width: 6%;'>Valor</th>
          <th scope="col" style='width: 8%;'>Data de Emissão</th>
          <th scope="col" style='width: 8%;'>CNPJ Emitente</th>
          <th scope="col" style='width: 12%;'>Nome do Emitente</th>
          <th scope="col" style='width: 8%;'>Chave do XML</th>
          <th scope="col" style='width: 8%;'>CNPJ Destinatário</th>
          <th scope="col" style='width: 12%;'>Nome Destinatário</th>
          <th scope="col" style='width: 1%;'>Ações</th>
        </tr>
      </thead>
    </table> 
    </div> 
  `,
    name: "Table",
    mounted() {
        this.initializeDataTable();
    },
    mounted() {
        const self = this;
        this.initializeDataTable();

        // Adicione um evento de escuta para a pesquisa na tabela
        $(this.$el).on("keyup", 'input[type="search"]', function () {
            self.dataTableInstance.search(this.value).draw();
        });
    },
    methods: {
        initializeDataTable() {
            const self = this;

            if (this.dataTableInstance) {
                this.dataTableInstance.destroy();
            }

            this.dataTableInstance = new DataTable("#myTable", {
                serverSide: true,
                responsive: true,
                pageLength: 50,
                paging: true,
                ordering: true,
                searching: true,
                processing: true,
                stateSave: true,
                order: [],
                ajax: {
                    url: "https://localhost:7196/XmlInfo",
                    type: "POST",
                    dataType: "json",
                    dataSrc: function (json) {
                        self.recordsTotal = json.recordsTotal; // Atualiza o número total de registros
                        return json.data;
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.error(errorThrown);
                    },
                },
                order: [[0, "desc"]],
                language: {
                    url: "https://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
                },

                lengthChange: false,
                initComplete: function () {
                    const pageInfo = self.dataTableInstance.page.info();
                    self.dataTableInstance.page(pageInfo.page).draw(false);
                    console.log(pageInfo)
                },
                columns: [
                    {
                        data: "type_xml",
                        render: function (data, type, row) {
                            switch (data) {
                                case 1:
                                    return "NFE";
                                case 2:
                                    return "NFCe";
                                case 3:
                                    return "CFE";
                                case 4:
                                    return "CTE";
                                default:
                                    return "Outros";
                            }
                        },
                    },
                    { data: "numberXml" },
                    {
                        data: "value",
                        render: function (data, type, row) {
                            return "R$ " + parseFloat(data).toFixed(2);
                        },
                    },
                    {
                        data: "dtEmit",
                        render: function (data, type, row) {
                            const dateObj = new Date(data);
                            const options = {
                                year: "numeric",
                                month: "2-digit",
                                day: "2-digit",
                                hour: "2-digit",
                                minute: "2-digit",
                                second: "2-digit"
                            };
                            return dateObj.toLocaleString("pt-BR", options).replace(",", "");
                        },
                    },
                    { data: "cnpjEmit" },
                    { data: "nameEmit" },
                    { data: "xmlKey" },
                    {
                        data: "cnpjDest",
                        render: function (data, type, row) {
                            return data ? data : "Não possui";
                        },
                    },
                    {
                        data: "nameDest",
                        render: function (data, type, row) {
                            return data ? data : "Não possui";
                        },
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return `
                <i class="fa fa-trash" aria-hidden="true" data-id="${data.id}"></i>
              `;
                        },
                    },
                ],
            });

            $(this.dataTableInstance.table().header()).on("click", "th", function () {
                self.handleOrdering();
            });

            $(this.dataTableInstance.table().body()).on("click", "i.fa-trash", function () {
                const itemId = $(this).data("id");
                self.excluirItem(itemId);
            });

            this.dataTableInstance.on('init.dt', function () {
                self.dataTableInstance
                    .columns()
                    .search(self.filtro !== null ? self.filtro : '')
                    .draw();
            });

            this.dataTableInstance.on("draw.dt", function () {
                self.handleStateSave();
            });

            $('#botao-apaga-tabela').on('click', function () {
                tabela.destroy();
                tabela = $('#myTable').DataTable({
                    
                });
            });

        },
        handleOrdering() {
            const pageInfo = this.dataTableInstance.page.info();
            const currentPage = pageInfo.page;
            const pageSize = pageInfo.length;
            const totalRecords = this.recordsTotal;

            const lastPage = Math.floor(this.lastPage * pageSize / totalRecords);

            this.dataTableInstance.page(lastPage).draw('page');
        },

        excluirItem(itemId) {
            const confirmed = confirm("Deseja realmente excluir o item com ID " + itemId + "?");
            if (confirmed) {
                axios
                    .delete("https://localhost:7196/XmlInfo/DeleteXml", {
                        params: {
                            id: itemId,
                        },
                    })
                    .then(() => {
                        window.alert("Item excluído com sucesso.");
                        this.initializeDataTable();
                    })
                    .catch((error) => {
                        console.error("Erro ao excluir o item:", error);
                    });
            }
        },
    },
    computed: {
        xmlsRequisicao() {
            return this.store.state.xmls.xmlsState;
        },
    },
    setup() {
        const store = UseStore();

        return {
            store
        };
    },
    data() {
        return {
            xmlData: [],
            showInput: false,
            dataTableInstance: null,
            filtro: null
        };
    },
};