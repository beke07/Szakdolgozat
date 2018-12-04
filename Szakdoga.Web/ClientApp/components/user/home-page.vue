<template>
  <div id="home">
    <h1>Üdvözöljük {{ user.lastName }}!</h1>
    <div class="row mt-5">

      <div class="col-sm-12 col-md-4 py-2">
        <div class="card h-100">
          <img class="card-img-top p-1" src="/images/projects.png" height="150" alt="Projektjeim">
          <div class="card-body d-flex align-items-end flex-wrap">
            <h5 class="card-title">Projektjeim</h5>
            <p class="card-text text-justify">Projektjeim megtekintése, taszkok hozzáadása</p>
            <router-link to="/user/projects" class="btn btn-primary">Megnézem</router-link>
          </div>
        </div>
      </div>

      <div class="col-sm-12 col-md-4 py-2">
        <div class="card h-100">
          <img class="card-img-top" src="/images/users.jpg" height="150" :alt="user.firstName">
          <div class="card-body d-flex align-items-end flex-wrap">
            <h5 class="card-title">{{ user.lastName }}</h5>
            <p class="card-text text-justify">Adataim megtekintése és szerkesztése, óráim megtekintése</p>
            <router-link to="/user/profil" class="btn btn-primary">Megnézem</router-link>
          </div>
        </div>
      </div>

      <div class="col-sm-12 col-md-4 py-2">
        <div class="card h-100">
          <img class="card-img-top p-1" src="/images/hierarchy.png" height="150" alt="Hierarchia">
          <div class="card-body d-flex align-items-end flex-wrap">
            <h5 class="card-title">Hierarchia</h5>
            <p class="card-text text-justify">Munkahelyi hierarchia megtekintése</p>
            <router-link to="/hierarchy" class="btn btn-primary">Megnézem</router-link>
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
    methods: {
    }
  }
</script>

<style lang="less" scoped>
  img {
    object-fit: cover;
  }

</style>
