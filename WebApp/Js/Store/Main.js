import { RETURN_XMLS, GET_XMLS, GET_FILTERS } from './Consts.js';
import http from '../protocols/AxiosConection.js';

export const key = Symbol();

export const store = Vuex.createStore({
    state: {
        xmls: {
            xmlsState: [],
            filtro: ''
        }
    },
    mutations: {
        [GET_XMLS](state, listaDeXmls) {
            state.xmls.xmlsState = listaDeXmls;
        },
        [GET_FILTERS](state, filtro) {
            state.xmls.filtro = filtro; 
        }
    },
    getters: {
        xmls(state) {
            return state.xmls;
        },
        filtro(state) {
            return state.filtro
        }
    },
    actions: {
        [RETURN_XMLS]({ commit }) {
            return http.get('https://localhost:7196/XmlInfo/ListXml').then(response => commit(GET_XMLS, response.data));
        }
    }
})

export function UseStore() {
    return Vuex.useStore(key);
}