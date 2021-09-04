FROM fedora:latest
RUN dnf install createrepo -y
COPY RepositoryManager/Scripts/RPMRepoInit.sh /RPMRepoInit.sh
RUN chmod +x /RPMRepoInit.sh
ENTRYPOINT [ "./RPMRepoInit.sh" ]
