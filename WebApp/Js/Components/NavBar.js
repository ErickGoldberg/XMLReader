﻿import { GET_FILTERS } from "../store/Consts.js";
import { UseStore } from "../Store/Main.js"

export default {
    template: `
    <header>
        <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
          <div class="container-fluid">
            <a class="navbar-brand fs-1" href="#"><img class="ms-2" style="width: 80px; height=80px;" src="https://cdn-icons-png.flaticon.com/512/136/136444.png"/></a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
              <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item" style="font-size: 40px;">
                    XML Reader
                </li>
              </ul>
              
            </div>
          </div>
        </nav>
    </header>    
    `,
    methods: {
            sendFilter() {
                this.store.commit(GET_FILTERS, this.filtro)
            }
        },
        setup() {
            const store = UseStore()
            return {
                store
            }
        }
}