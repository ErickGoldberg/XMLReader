import { UseStore } from '../store/Main.js';
import Modal from './Modal.js'

export default {
    components: { Modal },
    template: `
    <div style="display: flex; float: right; flex-direction: row-reverse; ">
      <modal/>
      <form class="d-flex me-3" role="search">
        <button v-if="!showInput" class="btn btn-outline-success" @click="mostrarInput()" style="color:#0D6efd; border-color:#0D6efd; width: 90px;" type="button">Pesquisar</button>
        <input v-if="showInput" class="form-control me-2" v-model="filtro" type="search" placeholder="Pesquisar xml" style="width: 200px;" aria-label="Search">
        <button v-if="showInput" class="btn btn-outline-success" @click="sendFilter()" style="color:#0D6efd; border-color:#0D6efd; width: 90px;" type="submit">Pesquisar</button>
      </form>
    </div>
    <table class="table table-striped table-hover display mx-2" id="myTable">
      <thead>
        <tr class="id={xmlData.id}">
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
                searching: true,
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
                <i class="fa fa-trash" aria-hidden="true" data-id="${data.id}"></i>
              `;
                        },
                    },
                ],
                data: this.xmlData,
                axios: {
                    url: `https://localhost:7196/XmlInfo`,
                    type: 'POST',
                    dataType: 'json',
                    data: function (data) {
                        console.log(data)
                    }
                }
            });

            const tableBody = table.table().body();

            $(tableBody).on("click", "i.fa-trash", function () {
                const itemId = $(this).data("id");
                self.excluirItem(itemId);
            });

        },
        mostrarInput() {
            this.showInput = true;
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
            showInput: false
        };
    },
};

