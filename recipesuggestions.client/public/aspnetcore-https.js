const fs = require('fs');
const path = require('path');

// Determine the certificate folder path
const baseFolder = process.env.APPDATA
  ? path.join(process.env.APPDATA, 'ASP.NET', 'https')
  : path.join(process.env.HOME || '', '.aspnet', 'https');

// Ensure the folder exists
fs.mkdirSync(baseFolder, { recursive: true });

const certificatePath = path.join(baseFolder, 'aspnetcore-https.pem');
const keyPath = path.join(baseFolder, 'aspnetcore-https-key.pem');

module.exports = {
  key: fs.existsSync(keyPath) ? fs.readFileSync(keyPath) : '',
  cert: fs.existsSync(certificatePath) ? fs.readFileSync(certificatePath) : '',
};
