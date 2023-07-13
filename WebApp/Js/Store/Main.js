
import { createStore, useStore } from 'vuex';
import { RETURN_XMLS, GET_XMLS } from './Consts.js';
import http from '../protocols/AxiosConection.js';

export const key = Symbol();

export const store = createStore({
    state() {
        return {
            xmls: {
                xmlsState: []
            }
        };
    },
    mutations: {
        [GET_XMLS](state, listaDeXmls) {
            state.xmls.xmlsState = listaDeXmls;
        }
    },
    getters: {
        xmls(state) {
            return state.xmls;
        }
    },
    actions: {
        [RETURN_XMLS]({ commit }) {
            return http.get('/file').then(response => commit(GET_XMLS, response.data));
        }
    }
});

export function UseStore() {
    return useStore(key);
}
