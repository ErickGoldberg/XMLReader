import { UseStore } from "../store/Main.js";

export default {
    template: `
    <div>
      <button class="btn btn-outline-success" @click="openModal" style="color:#0D6efd; border-color:#0D6efd; whidth: 50px; margin-right: 20px;">Inserir</button>
      <div v-if="isModalOpen" class="modal">
        <div class="modal-content">   
          <span class="modal-close" @click="closeModal">&times;</span>
          <h1 style="margin-bottom: 30px;">Inserir XML:</h1>
          <form @submit.prevent="sendXml" novalidate>
            <div class="mb-3">
              <label for="formFile" class="form-label" style="font-size: 20px;">Escolha o arquivo que quer inserir: </label>
              <input class="form-control" @change="onChangeFile" type="file" id="formFile">
            </div>
            <div class="col-12">
              <button class="btn btn-primary" type="submit" style="float: right">Inserir</button>
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
            }).then(res => console.log(res))
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
