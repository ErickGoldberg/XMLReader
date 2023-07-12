

export default {
    template: `
    <table class="table">
          <thead>
            <tr>
              <th scope="col">Chave do XML</th>
              <th scope="col">Tipo de XML</th>
              <th scope="col">Número do XML</th>              
              <th scope="col">Data de Emissão</th>
              <th scope="col">CNPJ Emitente</th>
              <th scope="col">Nome do Emitente</th>            
              <th scope="col">CNPJ do destinatário</th>
              <th scope="col">Nome do destinatário</th>
              <th scope="col">Valor</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(xml, index) of xmls" :key="index">
              <td scope="row">{{xml.}}</td>
              <td scope="row">{{  }}</td>
              <td scope="row">{{  }}</td>
              <td scope="row">{{  }}</td>
              <td scope="row">{{  }}</td>
              <td scope="row">{{  }}</td>
              <td scope="row">{{  }}</td>
              <td scope="row">{{  }}</td>
              <td scope="row">{{  }}</td>
              <td scope="row">{{  }}</td>
            </tr>
            
          </tbody>
        </table>
    `,
    name: "Tabela"