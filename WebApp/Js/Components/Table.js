import { RETURN_XMLS } from '../Store/Consts.js'
import { UseStore } from '../store/Main.js'

export default {
    template: `
    <table class="table table-striped display mx-2" id="myTable">
      <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col">Tipo de XML</th>
          <th scope="col">Número do XML</th>
          <th scope="col">Valor</th>
          <th scope="col">Data de Emissão</th>
          <th scope="col">CNPJ Emitente</th>
          <th scope="col">Nome do Emitente</th>
          <th scope="col">Chave do XML</th>
          <th scope="col">CNPJ do destinatário</th>
          <th scope="col">Nome do destinatário</th>
        </tr>
      </thead>  
    </table>
  `,
    name: "Table",
    mounted() {
        this.store.dispatch(RETURN_XMLS)
        this.exibirDataTable()
    },
    methods: {
        async exibirDataTable() {
            const response = await axios.get("http://localhost:5071/file")
            const data = await response.data
            const table = new DataTable("#myTable", {
                responsive: true,
                columnsDefs: [{
                    "defaultContent": "_",
                    "targets": "_all"
                }],
                columns: this.columns,
                data: data
            })
        }
    },
    computed: {
        xmlsRequisicao() {
            return this.store.state.xmls.xmlsState
        },
    },
    setup() {
        const store = UseStore()
        return {
            store
        }
    },
    data() {
        return {
            columns: [
                { data: "id" },
                { data: "type_xml" },
                { data: "numberXml" },
                { data: "value" },
                { data: "dtEmit" },
                { data: "cnpjEmit" },
                { data: "nameEmit" },
                { data: "xmlKey" },
                { data: "cnpjDest" },
                { data: "nameDest" },
            ]
        }
    },
}
