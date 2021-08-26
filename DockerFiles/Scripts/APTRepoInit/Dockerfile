FROM ubuntu:latest
COPY RepositoryManager/Scripts/APTRepoInit.sh /APTRepoInit.sh
RUN chmod +x /APTRepoInit.sh
ENTRYPOINT [ "/APTRepoInit.sh" ]