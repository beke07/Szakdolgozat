<template>
  <div id="home">

    <div class="row">
      <h1>{{project.title}}</h1>
    </div>

    <div class="row mt-3">
      <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
        <label class="m-0 display-4" for="deadline">Kezdet</label>
      </div>
      <div class="col-sm-12 offset-md-1 col-md-6">
        <label class="m-0 display-4 font-weight-bold" for="deadline">{{project.startTime}}</label>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
        <label class="m-0 display-4" for="deadline">Határidő</label>
      </div>
      <div class="col-sm-12 offset-md-1 col-md-6">
        <label class="m-0 display-4 font-weight-bold" for="deadline">{{project.deadline}}</label>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
        <label class="m-0 display-4" for="deadline">Becsült időigény</label>
      </div>
      <div class="col-sm-12 offset-md-1 col-md-6">
        <label class="m-0 display-4 font-weight-bold" for="deadline">{{project.sumWorkHours}} óra</label>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
        <label class="m-0 display-4" for="deadline">Ráfordított órák</label>
      </div>
      <div class="col-sm-12 offset-md-1 col-md-6">
        <label class="m-0 display-4 font-weight-bold" for="deadline">{{project.sumTaskHours}} óra</label>
      </div>
    </div>


    <div class="row mt-3">
      <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
        <label class="m-0 display-4" for="deadline">Készültség</label>
      </div>
      <div class="col-sm-12 offset-md-1 col-md-6">
        <b-progress :value="project.sumTaskHours" v-bind:variant="color" :max="project.sumWorkHours" show-progress animated>
          <b-progress-bar class="text-dark" :value="project.sumTaskHours">
            <strong>{{ procent }} %</strong>
          </b-progress-bar>
        </b-progress>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
        <label class="m-0 display-4" for="workers">Projekten dolgozók</label>
      </div>

      <div v-for="(worker, index) in project.workersData" :class="{'offset-md-1' : index === 0, 'offset-md-6' : index !== 0 }" class="col-sm-12 col-md-6">
        <label class="m-0 display-4 font-weight-bold" for="workers">{{worker.firstName}} {{worker.lastName}}</label>
      </div>

    </div>

  </div>
</template>
<script>
  import { projectService } from "../../../services/project-service";
  import { commonService } from "../../../services/common-service";

  export default {
    data() {
      return {
        project: {}
      }
    },
    computed: {
      procent: function () {
        return (this.project.sumTaskHours / this.project.sumWorkHours * 100).toFixed(2);
      },
      color: function () {
        if (this.procent < 25) {
          return 'danger';
        }
        else if (this.procent < 75) {
          return 'warning';
        }
        else if (this.procent <= 100) {
          return 'success';
        }
      }
    },
    methods: {
      loadProject: function () {

        let id = this.$route.params.id;
        let app = this;

        projectService.getProjectDetails(id)
          .then(function (res) {
            app.project = res.data;
            app.project.deadline = commonService.dateTimeformat(app.project.deadline);
            app.project.startTime = commonService.dateTimeformat(app.project.startTime);
          })
          .catch(function (err) {
            console.error(err);
          });
      }
    },
    created() {
      this.loadProject();
    }
  }
</script>

<style lang="less" scoped>
  .progress-bar strong {
    padding-left: 10px;
  }

  .progress {
    height: 30px;
  }
</style>
