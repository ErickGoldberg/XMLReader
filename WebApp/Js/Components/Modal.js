import '../../Styles/styles.css';

export default {
    template: `

    <div>
    <button @click="openModal">Open Modal</button>
    <div v-if="isModalOpen" class="modal">
      <div class="modal-content">
        <span class="modal-close" @click="closeModal">&times;</span>
        <h2>Modal Content</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
      </div>
    </div>
  </div>

    `,
    name: "Modal",
    data() {
        return {
            isModalOpen: false
        }
    },
    methods: {
        openModal() {
            this.isModalOpen = true;
        },
        closeModal() {
            this.isModalOpen = false;
        }
    }
}
