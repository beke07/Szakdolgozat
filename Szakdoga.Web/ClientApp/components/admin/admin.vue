<template>
  <div id="admin">
    <h1>Üdvözöljük {{ user.lastName }}!</h1>
    <div class="row mt-5">

      <div class="col-sm-12 col-md-4 py-2">
        <div class="card h-100">
          <img class="card-img-top p-1" src="/images/users.jpg" height="150" alt="Munkatársak">
          <div class="card-body d-flex align-items-end flex-wrap">
            <h5 class="card-title">Munkatársak</h5>
            <p class="card-text text-justify">A cégnél dolgozó munkatársak adatainak megtekintése és új munkatárs felvétele</p>
            <router-link to="/admin/users" class="btn btn-primary">Megnézem</router-link>
          </div>
        </div>
      </div>

      <div class="col-sm-12 col-md-4 py-2">
        <div class="card h-100">
          <img class="card-img-top p-1" src="/images/projects.png" height="150" alt="Projektek">
          <div class="card-body d-flex align-items-end flex-wrap">
            <h5 class="card-title">Projektek</h5>
            <p class="card-text text-justify">Projektek adatainak megtekintése és új projekt felvétele</p>
            <router-link to="/admin/projects" class="btn btn-primary">Megnézem</router-link>
          </div>
        </div>
      </div>

      <div class="col-sm-12 col-md-4 py-2">
        <div class="card h-100">
          <img class="card-img-top p-1" src="/images/hierarchy.png" height="150" alt="Hierarchia">
          <div class="card-body d-flex align-items-end flex-wrap">
            <h5 class="card-title">Hierarchia</h5>
            <p class="card-text text-justify">Munkatársak hiereachiájának megtekintése illetve szerkesztése</p>
            <router-link to="/admin/hierarchy" class="btn btn-primary">Megnézem</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

  export default {
    data() {
      return {
        user: JSON.parse(window.localStorage.getItem('lbUser')).data
      }
    },

    computed: {
    },
    methods: {

    }

  }
</script>

<style scoped>
  img {
    object-fit: cover;
  }
</style>
