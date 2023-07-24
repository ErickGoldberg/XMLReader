import { UseStore } from "../store/Main.js";

export default {
    props: {
        dataTableRef: Object, 
    },
    template: `
    <div>
      <button class="btn btn-outline-success" @click="openModal" style="color:#0D6efd; border-color:#0D6efd; whidth: 50px;  float:right; margin-right:10px; ">Inserir</button>
      <div v-if="isModalOpen" class="modal">
        <div class="modal-content">   
          <span class="modal-close" @click="closeModal" style="float: right; border-color: white;">&times;</span>
          <h1 style="margin-bottom: 30px; color: white;">Inserir XML:</h1>
          <form @submit.prevent="sendXml" novalidate>
            <div class="mb-3">
              <label for="formFile" class="form-label" style="font-size: 20px; color: white;">Escolha o arquivo que quer inserir: </label>
              <input class="form-control" @change="onChangeFile" type="file" id="formFile">
            </div>
            <div class="col-12">
              <button class="btn btn-primary" type="submit" style="float: right; border-color: white;">Inserir</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  `,
    name: "Modal", 
    data() {
        return {
            isModalOpen: false,
            file: ''
        }
    },
    methods: {
        openModal() {
            this.isModalOpen = true;
        },
        closeModal() {
            this.isModalOpen = false;
        },
        sendXml() {
            this.enviarXMLParaAPI();
            this.closeModal();
        },
        enviarXMLParaAPI() {
            let formData = new FormData();
            formData.append('file', this.file)
            axios.post('https://localhost:7196/XmlInfo/PostXml', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            }).then(() => {
                window.alert("Cadastro realizado com sucesso.");
                this.dataTableRef.initializeDataTable();
                this.closeModal();
            })
        },  
        onChangeFile(event) {
            this.file = event.target.files[0];
            console.log(this.file)
        }
    },
    setup() {
        const store = UseStore()
        return {
            store
        }
    }
}
