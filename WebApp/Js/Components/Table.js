import { UseStore } from '../store/Main.js';
import Modal from './Modal.js';

export default {
    components: { Modal },
    template: `
    <div class="overlay">
      <modal />
    </div> 
    <div class="table-container">
    <table class="table table-striped table-hover display mx-2" id="myTable">
      <thead>
        <tr>
          <th scope="col">Tipo de XML</th>
          <th scope="col">Número do XML</th>
          <th scope="col">Valor</th>
          <th scope="col">Data de Emissão</th>
          <th scope="col">CNPJ Emitente</th>
          <th scope="col">Nome do Emitente</th>
          <th scope="col">Chave do XML</th>
          <th scope="col">CNPJ Destinatário</th>
          <th scope="col">Nome Destinatário</th>
          <th scope="col">Ações</th>
        </tr>
      </thead>
    </table> 
    </div> 
  `,
    name: "Table",
    mounted() {
        this.initializeDataTable();
    },
    methods: {
        initializeDataTable() {
            const self = this;

            if (this.dataTableInstance) {
                this.dataTableInstance.destroy();
            }

            this.dataTableInstance = new DataTable("#myTable", {
                serverSide: true,
                ajax: {
                    url: "https://localhost:7196/XmlInfo/ListXml",
                    type: "GET", // Use GET method for server-side rendering
                    dataType: "json",
                    data: function (data) {
                        // No need to add any data here, as DataTables will automatically send the necessary parameters for server-side processing.
                    },
                    dataSrc: function (json) {
                        return json.data; // Assuming your API returns an array with 'data' property
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.error(errorThrown);
                    },
                },
                order: [[0, "desc"]],
                language: {
                    url: "https://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
                },
                responsive: true,
                searching: true,
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
            store,
        };
    },
    data() {
        return {
            xmlData: [],
            showInput: false,
            dataTableInstance: null,
        };
    },
};