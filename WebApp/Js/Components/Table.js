//import { useStore } from '../Store/Main'

export default {
    template: `
    <table class="table">
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
          <tbody>
            <tr >
              <td scope="row">teste</td>
              <td scope="row"></td>
              <td scope="row"></td>
              <td scope="row"></td>
              <td scope="row"></td>
              <td scope="row"></td>
              <td scope="row"></td>
              <td scope="row"></td>
              <td scope="row"></td>
              <td scope="row"></td>
              <td></td>
            </tr>
          </tbody>
        </table>
  `,
    name: "Table"
};

