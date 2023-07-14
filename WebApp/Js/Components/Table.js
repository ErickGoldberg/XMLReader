import { UseStore } from '../store/Main.js';

export default {
    template: `
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
          <th scope="col">CNPJ do destinatário</th>
          <th scope="col">Nome do destinatário</th>
          <th scope="col">Ações</th>
        </tr>
      </thead>
    </table>
  `,
    name: "Table",
    mounted() {
        this.ExibirDataTable();
    },
    methods: {
        ExibirDataTable() {
            axios
                .get("https://localhost:7196/XmlInfo/ListXml")
                .then((response) => {
                    this.xmlData = response.data;
                    this.exibirDataTable();
                })
                .catch((error) => {
                    console.error(error);
                });
        },
        exibirDataTable() {
            const self = this; // Armazena a referência do componente atual

            const table = new DataTable("#myTable", {
                responsive: true,
                searching: false,
                columnsDefs: [
                    {
                        defaultContent: "_",
                        targets: "_all",
                    },
                ],
                columns: [
                    { data: "type_xml" },
                    { data: "numberXml" },
                    { data: "value" },
                    { data: "dtEmit" },
                    { data: "cnpjEmit" },
                    { data: "nameEmit" },
                    { data: "xmlKey" },
                    { data: "cnpjDest" },
                    { data: "nameDest" },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return `
                <i class="fa fa-trash" aria-hidden="true" data-id="${row.id}"></i>
              `;
                        },
                    },
                ],
                data: this.xmlData,
            });

            // Adiciona um ouvinte de evento de clique na tabela
            table.on("click", "i.fa-trash", function () {
                const itemId = this.getAttribute("data-id");
                self.excluirItem(itemId);
            });
        },
        excluirItem(itemId) {
            const confirmed = confirm("Deseja realmente excluir o item com ID " + itemId + "?");
            if (confirmed) {
                axios
                    .delete("https://localhost:7196/XmlInfo/DeleteXml/" + itemId)
                    .then(() => {
                        console.log("Item excluído com sucesso.");

                        const index = this.xmlData.findIndex((item) => item.id === itemId);
                        if (index > -1) {
                            this.xmlData.splice(index, 1);
                        }
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
        };
    },
};
