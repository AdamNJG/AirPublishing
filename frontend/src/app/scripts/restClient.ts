import axios from 'axios';

class restClient {
  static baseUrl = 'https://localhost:7207';

  static async postImage (userId: string, imageData: string, fileName: string) {
    try {
      const response = await axios.post(`${this.baseUrl}/api/ImageVerification`, {
        userId: userId,
        imageData: imageData,
        fileName: fileName
      });
      return response;
    }
    catch (error) {
      console.log(error);
    }
  }
}

export default restClient;