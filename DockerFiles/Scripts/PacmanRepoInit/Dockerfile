FROM archlinux:latest
COPY RepositoryManager/Scripts/PacmanRepoInit.sh /PacmanRepoInit.sh
RUN chmod +x /PacmanRepoInit.sh
ENTRYPOINT [ "./PacmanRepoInit.sh" ]