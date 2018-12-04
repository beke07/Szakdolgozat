import axios from 'axios';
import { commonService } from './common-service';

class RankService {

  getRanks() {
    return axios.get("/Rank/GetRanks");
  }

  deleteRank(id) {
    return axios.delete("/Rank/DeleteRank", { params: { id: id } });
  }

  createRank(name) {
    return axios.post("/Rank/CreateRank", commonService.toFormData({ name: name }));
  }
}

export const rankService = new RankService();
