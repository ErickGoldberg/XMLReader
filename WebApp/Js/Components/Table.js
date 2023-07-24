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
          <th scope="col" style='width: 8%;'>Tipo de XML</th>
          <th scope="col" style='width: 8%;'>Número do XML</th>
          <th scope="col" style='width: 8%;'>Valor</th>
          <th scope="col" style='width: 8%;'>Data de Emissão</th>
          <th scope="col" style='width: 8%;'>CNPJ Emitente</th>
          <th scope="col" style='width: 8%;'>Nome do Emitente</th>
          <th scope="col" style='width: 8%;'>Chave do XML</th>
          <th scope="col" style='width: 8%;'>CNPJ Destinatário</th>
          <th scope="col" style='width: 8%;'>Nome Destinatário</th>
          <th scope="col" style='width: 8%;'>Ações</th>
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
                pageLength: 15,
                paging: true,
                ordering: true,
                searching: true,
                processing: true,
                ajax: {
                    url: "https://localhost:7196/XmlInfo",
                    type: "POST", 
                    dataType: "json",
                    data: function (data) {
                       
                    },
                    dataSrc: function (json) {
                        return json.data; // JSON que contém os dados retornados pelo servidor
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

            $(this.dataTableInstance.table().body()).on("click", "i.fa-trash", function () {
                const itemId = $(this).data("id");
                self.excluirItem(itemId);
            });

            this.dataTableInstance.on('init.dt', function () {
                // Define a opção searchCols com o valor de pesquisa padrão para todas as colunas
                self.dataTableInstance
                    .columns()
                    .search(self.filtro !== null ? self.filtro : '')
                    .draw();
            });
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