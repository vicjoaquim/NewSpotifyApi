const API_URL = "http://localhost:5005/api/Musicas";

document.addEventListener("DOMContentLoaded", () => {
  const btnCadastro = document.getElementById("btnCadastro");
  const btnLista = document.getElementById("btnLista");
  const telaCadastro = document.getElementById("telaCadastro");
  const telaLista = document.getElementById("telaLista");
  const form = document.getElementById("musicaForm");
  const lista = document.getElementById("musicaList");

  btnCadastro.addEventListener("click", () => {
    telaCadastro.classList.add("active");
    telaLista.classList.remove("active");
    btnCadastro.classList.add("active");
    btnLista.classList.remove("active");
  });

  btnLista.addEventListener("click", () => {
    telaCadastro.classList.remove("active");
    telaLista.classList.add("active");
    btnLista.classList.add("active");
    btnCadastro.classList.remove("active");
    carregarLista();
  });

  async function carregarLista() {
    lista.innerHTML = "<p>Carregando...</p>";
    try {
      const res = await fetch(`${API_URL}/Listar`);
      const data = await res.json();

      if (!data || data.length === 0) {
        lista.innerHTML = "<p>Nenhuma música cadastrada.</p>";
        return;
      }

      lista.innerHTML = "";
      data.forEach(musica => {
        const item = document.createElement("div");
        item.classList.add("musica-card");
        item.innerHTML = `
          <img src="http://localhost:5005${musica.imagem || '/logo.png'}" alt="Capa">
          <h3>${musica.nomeMusica}</h3>
          <p>${musica.artistaMusica}</p>
          <audio controls src="http://localhost:5005${musica.arquivo}"></audio>
        `;
        lista.appendChild(item);
      });
    } catch (err) {
      console.error(err);
      lista.innerHTML = "<p>Erro ao carregar músicas.</p>";
    }
  }

  form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const nome = form.nomeMusica.value;
    const artista = form.artistaMusica.value;
    const arquivo = form.arquivo.files[0];
    const imagem = form.imagem.files[0];

    const formData = new FormData();
    formData.append("nomeMusica", nome);
    formData.append("artistaMusica", artista);
    formData.append("arquivo", arquivo);
    if (imagem) formData.append("imagem", imagem);

    try {
      const res = await fetch(`${API_URL}/Cadastrar`, {
        method: "POST",
        body: formData
      });
      const data = await res.json();

      if (res.ok) {
        alert("Música cadastrada com sucesso!");
        form.reset();
        btnLista.click();
      } else {
        alert("Erro: " + (data.erro || "Não foi possível cadastrar a música."));
      }
    } catch (err) {
      console.error(err);
      alert("Erro ao cadastrar a música.");
    }
  });

  btnLista.click(); // Carrega lista inicialmente
});
